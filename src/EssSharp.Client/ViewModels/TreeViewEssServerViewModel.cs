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

            LoadChildrenCommand = new Command<IEssNode>(( node ) => node.GetChildren());
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

            public ObservableCollection<IEssNode> GetChildren();
        }

        public enum NodeType
        {
            Unknown,
            Server,
            Application,
            Cube
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

            public virtual ObservableCollection<IEssNode> GetChildren()
            {
                var children = new ObservableCollection<IEssNode>();

                IsLeaf = children.Count == 0;
                return Children = children;
            }
        }

        public class EssServerNode : EssNode
        {
            public IEssServer Server { get; set; }

            public override string Name => Server.Name;

            public override NodeType Type => NodeType.Server;

            public override ObservableCollection<IEssNode> GetChildren()
            {
                var children = new ObservableCollection<IEssNode>();

                foreach ( var application in Server.GetApplications() )
                    if ( application is { } )
                        children.Add(new EssApplicationNode() { Application = application });

                IsLeaf = children.Count == 0;
                return Children = children;
            }
        }

        public class EssApplicationNode : EssNode
        {
            public IEssApplication Application { get; set; }

            public override string Name => Application.Name;

            public override NodeType Type => NodeType.Application;

            public override ObservableCollection<IEssNode> GetChildren()
            {
                var children = new ObservableCollection<IEssNode>();

                foreach ( var cube in Application.GetCubes() )
                    if ( cube is { } )
                        children.Add(new EssCubeNode() { Cube = cube, IsLeaf = true });

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
