using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Extensions.Configuration;
using UraniumUI;

namespace EssSharp.Client.ViewModels
{
    public class TreeViewEssServerViewModel : UraniumBindableObject
    {
        IConfiguration _configuration;

        public ObservableCollection<IEssNode> Nodes { get; private set; }

        public ICommand LoadChildrenCommand { get; set; }

        public TreeViewEssServerViewModel()
        {
        }

        public TreeViewEssServerViewModel( IConfiguration configuration )
        {
            _configuration = configuration;

            InitializeNodes();

            LoadChildrenCommand = new Command<IEssNode>(async (node) => await node.GetChildrenAsync(new CancellationTokenSource(TimeSpan.FromSeconds(10)).Token));
        }

        void InitializeNodes()
        {
            var connections = _configuration?.GetSection("Settings")?.Get<AppSettings>()?.Connections;
            var nodes = new ObservableCollection<IEssNode>();

            if ( connections?.Length > 0 )
            {
                foreach ( var connection in connections )
                    if ( !string.IsNullOrEmpty(connection?.Server) )
                        nodes.Add(new EssServerNode() { Server = new EssServer(connection.Server, connection.Username, connection.Password) });
            }

            Nodes = nodes;
        }

        public interface IEssNode
        {
            public ObservableCollection<IEssNode> Children { get; }

            public bool IsLeaf { get; set; }

            public string Name { get; }

            public NodeType Type { get; }

            public Task<ObservableCollection<IEssNode>> GetChildrenAsync( CancellationToken cancellationToken = default );
        }

        public enum NodeType
        {
            Unknown,
            Server,
            Application,
            Cube,
            Url,
            Folder
        }

        public class EssNode : UraniumBindableObject, IEssNode
        {
            protected ObservableCollection<IEssNode> _children;
            protected bool _isLeaf;

            public IEssObject EssObject { get; set; }

            public virtual string Name => EssObject.Name;

            public virtual NodeType Type => NodeType.Unknown;

            public bool IsLeaf { get => _isLeaf; set => SetProperty(ref _isLeaf, value); }

            public ObservableCollection<IEssNode> Children { get => _children; set => SetProperty(ref _children, value); }

            public virtual Task<ObservableCollection<IEssNode>> GetChildrenAsync( CancellationToken cancellationToken = default )
            {
                var children = new ObservableCollection<IEssNode>();

                IsLeaf = children.Count == 0;
                return Task.FromResult(Children = children);
            }
        }

        public class EssServerNode : EssNode
        {
            public IEssServer Server { get; set; }

            public override string Name => Server.Name;

            public override NodeType Type => NodeType.Server;

            public override Task<ObservableCollection<IEssNode>> GetChildrenAsync( CancellationToken cancellationToken = default )
            {
                var children = new ObservableCollection<IEssNode>
                {
                    new EssServerApplicationsNode() { Server = Server },
                    new EssServerUrlsNode() { Server = Server }
                };

                IsLeaf = children.Count == 0;
                return Task.FromResult(Children = children);
            }
        }

        public class EssServerApplicationsNode : EssNode
        {
            public IEssServer Server { get; set; }

            public override string Name => "Applications";

            public override NodeType Type => NodeType.Folder;

            public override async Task<ObservableCollection<IEssNode>> GetChildrenAsync( CancellationToken cancellationToken = default )
            {
                var children = new ObservableCollection<IEssNode>();

                try
                {
                    foreach ( var application in await Server.GetApplicationsAsync(cancellationToken) )
                        if ( application is { } )
                            children.Add(new EssApplicationNode() { Application = application });
                }
                catch
                {
                    // swallow
                }

                IsLeaf = children.Count == 0;
                return Children = children;
            }
        }

        public class EssServerUrlsNode : EssNode
        {
            public IEssServer Server { get; set; }

            public override string Name => "Urls";

            public override NodeType Type => NodeType.Folder;

            public override async Task<ObservableCollection<IEssNode>> GetChildrenAsync( CancellationToken cancellationToken = default )
            {
                var children = new ObservableCollection<IEssNode>();

                try
                {
                    foreach ( var url in await Server.GetURLsAsync(cancellationToken) )
                        if ( url is { } )
                            children.Add(new EssUrlNode() { Url = url, IsLeaf = true });
                }
                catch
                {
                    // swallow
                }

                IsLeaf = children.Count == 0;
                return Children = children;
            }
        }

        public class EssUrlNode : EssNode
        {
            public IEssUrl Url { get; set; }

            public override string Name => $@"{Url?.Name} ({Url?.Path})";

            public override NodeType Type => NodeType.Url;
        }

        public class EssApplicationNode : EssNode
        {
            public IEssApplication Application { get; set; }

            public override string Name => Application.Name;

            public override NodeType Type => NodeType.Application;

            public override async Task<ObservableCollection<IEssNode>> GetChildrenAsync( CancellationToken cancellationToken = default )
            {
                var children = new ObservableCollection<IEssNode>();

                try
                {
                    foreach ( var cube in await Application.GetCubesAsync(cancellationToken) )
                        if ( cube is { } )
                            children.Add(new EssCubeNode() { Cube = cube, IsLeaf = true });
                }
                catch
                {
                    // swallow
                }

                IsLeaf = children.Count == 0;
                return Children = children;
            }
        }

        public class EssCubeNode : EssNode
        {
            public IEssCube Cube { get; set; }

            public override string Name => Cube.Name;

            public override NodeType Type => NodeType.Cube;
        }
    }
}
