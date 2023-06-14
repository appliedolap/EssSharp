using System.Collections.ObjectModel;
using System.Windows.Input;

using Microsoft.Extensions.Configuration;
using UraniumUI;

namespace EssSharp.Client.ViewModels
{
    /// <summary />
    public class EssServerTreeViewModel : UraniumBindableObject
    {
        #region Private Data

        private readonly IConfiguration    _configuration;
        private readonly IEssServerFactory _factory;

        #endregion 

        #region Constructors

        /// <summary />
        /// <param name="configuration" />
        /// <param name="factory" />
        /// <exception cref="ArgumentNullException" />
        public EssServerTreeViewModel( IEssServerFactory factory, IConfiguration configuration = null )
        {
            _factory       = factory ?? throw new ArgumentNullException(nameof(factory), $"An {nameof(IEssServerFactory)} must be provided in order to fill the navigation tree." );
            _configuration = configuration;

            InitializeNodes();

            // TODO: figure out how to do asynchronous ICommand with reasonable cancellation and exception handling.
            LoadChildrenCommand = new Command<IEssNode>(async (node) => 
            {
                try
                {
                    await node.GetChildrenAsync(new CancellationTokenSource(TimeSpan.FromSeconds(30)).Token);
                }
                catch ( Exception e )
                {
                    try { await Application.Current.MainPage.DisplayAlert("whoops", e.Message, "OK"); } catch { }
                }
            });

        }

        #endregion

        #region Public Properties

        /// <summary />
        public ObservableCollection<IEssNode> Nodes { get; private set; }

        /// <summary />
        public ICommand LoadChildrenCommand { get; set; }

        #endregion

        #region Private Methods

        private void InitializeNodes()
        {
            var nodes = new ObservableCollection<IEssNode>();

            if ( _configuration?.GetSection("Settings")?.Get<AppSettings>()?.Connections is { Length: > 0 } connections )
            {
                foreach ( var connection in connections )
                    if ( !string.IsNullOrEmpty(connection?.Server) )
                        nodes.Add(new EssServerNode() { Server = _factory.CreateEssServer(connection.Server, connection.Username, connection.Password, false) });
            }

            Nodes = nodes;
        }

        #endregion

        #region Private Classes

        private class EssNode : UraniumBindableObject, IEssNode
        {
            protected ObservableCollection<IEssNode> _children;
            protected bool _isLeaf;

            public IEssObject EssObject { get; set; }

            public virtual string Name => EssObject.Name;

            public virtual EssNodeType Type => EssNodeType.Unknown;

            public bool IsLeaf { get => _isLeaf; set => SetProperty(ref _isLeaf, value); }

            public ObservableCollection<IEssNode> Children { get => _children; set => SetProperty(ref _children, value); }

            public virtual Task<ObservableCollection<IEssNode>> GetChildrenAsync( CancellationToken cancellationToken = default )
            {
                var children = new ObservableCollection<IEssNode>();

                IsLeaf = children.Count == 0;
                return Task.FromResult(Children = children);
            }
        }

        private class EssServerNode : EssNode
        {
            public IEssServer Server { get; set; }

            public override string Name => Server.Name;

            public override EssNodeType Type => EssNodeType.Server;

            public override async Task<ObservableCollection<IEssNode>> GetChildrenAsync( CancellationToken cancellationToken = default )
            {
                var children = new ObservableCollection<IEssNode>();

                try
                {
                    await Server.GetUserSessionAsync(false, false, cancellationToken);

                    children = new ObservableCollection<IEssNode>
                    {
                        new EssServerApplicationsNode() { Server = Server },
                        new EssServerJobsNode()         { Server = Server },
                        new EssServerUrlsNode()         { Server = Server },
                        new EssServerVariablesNode()    { Server = Server }
                    };
                }
                catch ( Exception e )
                {
                    try { await Application.Current.MainPage.DisplayAlert("whoops", e.Message, "OK"); } catch { }
                }

                return Children = children;
            }
        }

        private class EssServerApplicationsNode : EssNode
        {
            public IEssServer Server { get; set; }

            public override string Name => "Applications";

            public override EssNodeType Type => EssNodeType.Folder;

            public override async Task<ObservableCollection<IEssNode>> GetChildrenAsync( CancellationToken cancellationToken = default )
            {
                var children = new ObservableCollection<IEssNode>();

                foreach ( var application in await Server.GetApplicationsAsync(cancellationToken) )
                    if ( application is { } )
                        children.Add(new EssApplicationNode() { Application = application });

                IsLeaf = children.Count == 0;
                return Children = children;
            }
        }

        private class EssServerJobsNode : EssNode
        {
            public IEssServer Server { get; set; }

            public override string Name => "Jobs";

            public override EssNodeType Type => EssNodeType.Folder;

            public override async Task<ObservableCollection<IEssNode>> GetChildrenAsync( CancellationToken cancellationToken = default )
            {
                var children = new ObservableCollection<IEssNode>();

                foreach ( var job in await Server.GetJobsAsync(5, cancellationToken) )
                    children.Add(new EssJobNode() { Job = job, IsLeaf = true });

                IsLeaf = children.Count == 0;
                return Children = children;
            }
        }

        private class EssServerUrlsNode : EssNode
        {
            public IEssServer Server { get; set; }

            public override string Name => "Urls";

            public override EssNodeType Type => EssNodeType.Folder;

            public override async Task<ObservableCollection<IEssNode>> GetChildrenAsync( CancellationToken cancellationToken = default )
            {
                var children = new ObservableCollection<IEssNode>();

                foreach ( var url in await Server.GetURLsAsync(cancellationToken) )
                    if ( url is { } )
                        children.Add(new EssUrlNode() { Url = url, IsLeaf = true });

                IsLeaf = children.Count == 0;
                return Children = children;
            }
        }

        private class EssServerVariablesNode : EssNode
        {
            public IEssServer Server { get; set; }

            public override string Name => "Variables";

            public override EssNodeType Type => EssNodeType.Folder;

            public override async Task<ObservableCollection<IEssNode>> GetChildrenAsync( CancellationToken cancellationToken = default )
            {
                var children = new ObservableCollection<IEssNode>();

                foreach ( var variable in await Server.GetVariablesAsync(cancellationToken) )
                    if ( variable is { } )
                        children.Add(new EssVariableNode() { Variable = variable, IsLeaf = true });

                IsLeaf = children.Count == 0;
                return Children = children;
            }
        }

        private class EssApplicationNode : EssNode
        {
            public IEssApplication Application { get; set; }

            public override string Name => Application.Name;

            public override EssNodeType Type => EssNodeType.Application;

            public override Task<ObservableCollection<IEssNode>> GetChildrenAsync( CancellationToken cancellationToken = default )
            {
                var children = new ObservableCollection<IEssNode>
                {
                    new EssApplicationCubesNode()     { Application = Application },
                    new EssApplicationVariablesNode() { Application = Application }
                };

                IsLeaf = children.Count == 0;
                return Task.FromResult(Children = children);
            }
        }

        private class EssApplicationCubesNode : EssNode
        {
            public IEssApplication Application { get; set; }

            public override string Name => "Cubes";

            public override EssNodeType Type => EssNodeType.Folder;

            public override async Task<ObservableCollection<IEssNode>> GetChildrenAsync( CancellationToken cancellationToken = default )
            {
                var children = new ObservableCollection<IEssNode>();

                foreach ( var cube in await Application.GetCubesAsync(cancellationToken) )
                    if ( cube is { } )
                        children.Add(new EssCubeNode() { Cube = cube });

                IsLeaf = children.Count == 0;
                return Children = children;
            }
        }

        private class EssApplicationVariablesNode : EssServerVariablesNode
        {
            public IEssApplication Application { get; set; }

            public override async Task<ObservableCollection<IEssNode>> GetChildrenAsync( CancellationToken cancellationToken = default )
            {
                var children = new ObservableCollection<IEssNode>();

                foreach ( var variable in await Application.GetVariablesAsync(cancellationToken) )
                    if ( variable is { } )
                        children.Add(new EssVariableNode() { Variable = variable, IsLeaf = true });

                IsLeaf = children.Count == 0;
                return Children = children;
            }
        }

        private class EssCubeNode : EssNode
        {
            public IEssCube Cube { get; set; }

            public override string Name => Cube.Name;

            public override EssNodeType Type => EssNodeType.Cube;

            public override Task<ObservableCollection<IEssNode>> GetChildrenAsync( CancellationToken cancellationToken = default )
            {
                var children = new ObservableCollection<IEssNode>
                {
                    new EssCubeVariablesNode() { Cube = Cube }
                };

                IsLeaf = children.Count == 0;
                return Task.FromResult(Children = children);
            }
        }

        private class EssCubeVariablesNode : EssApplicationVariablesNode
        {
            public IEssCube Cube { get; set; }

            public override async Task<ObservableCollection<IEssNode>> GetChildrenAsync( CancellationToken cancellationToken = default )
            {
                var children = new ObservableCollection<IEssNode>();

                foreach ( var variable in await Cube.GetVariablesAsync(cancellationToken) )
                    if ( variable is { } )
                        children.Add(new EssVariableNode() { Variable = variable, IsLeaf = true });

                IsLeaf = children.Count == 0;
                return Children = children;
            }
        }

        private class EssJobNode : EssNode
        {
            public IEssJob Job { get; set; }

            public override string Name => $@"{Job?.Name} - {Job?.JobType.ToDescription()} - {Job?.JobStatus}";

            public override EssNodeType Type => EssNodeType.Job;
        }

        private class EssUrlNode : EssNode
        {
            public IEssUrl Url { get; set; }

            public override string Name => $@"{Url?.Name} ({Url?.Path})";

            public override EssNodeType Type => EssNodeType.Url;
        }

        private class EssVariableNode : EssNode
        {
            public IEssServerVariable Variable { get; set; }

            public override string Name => $@"{Variable?.Name} = ""{Variable?.Value}""";

            public override EssNodeType Type => EssNodeType.Variable;
        }

        #endregion
    }

    /// <summary />
    public interface IEssNode
    {
        /// <summary />
        public ObservableCollection<IEssNode> Children { get; }

        /// <summary />
        public bool IsLeaf { get; set; }

        /// <summary />
        public string Name { get; }

        /// <summary />
        public EssNodeType Type { get; }

        /// <summary />
        /// <param name="cancellationToken" />
        public Task<ObservableCollection<IEssNode>> GetChildrenAsync( CancellationToken cancellationToken = default );
    }

    /// <summary />
    public enum EssNodeType
    {
        Unknown,
        Server,
        Application,
        Cube,
        Url,
        Variable,
        Folder,
        Job
    }
}
