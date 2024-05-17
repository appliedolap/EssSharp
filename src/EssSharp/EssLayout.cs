using EssSharp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EssSharp
{
    /// <summary />
    public class EssLayout : EssObject, IEssLayout
    {
        #region Private Data

        private readonly EssCube _cube;
        private GridLayout _layout;

        #endregion

        #region Constructors

        /// <summary />
        internal EssLayout( GridLayout layout, EssCube cube ) : base(cube?.Configuration, cube?.Client)
        {
            _layout = layout ??
                throw new ArgumentNullException(nameof(layout), $"An API model {nameof(layout)} is required to create an {nameof(EssLayout)}.");

            _cube = cube ??
                throw new ArgumentNullException(nameof(cube), $"An {nameof(EssCube)} {nameof(cube)} is required to create an {nameof(EssLayout)}.");
        }

        #endregion

        /// <inheritdoc />
        public override string Name => _layout.Alias;

        /// <inheritdoc />
        public override EssType Type => EssType.Layout;

        /// <inheritdoc />
        /// TODO: is this even needed?
        public string Alias => Name;

        /// <inheritdoc />
        public EssGridLayoutData Data => _layout.Data.ToEssSharpObject();

        /// <inheritdoc />
        public List<EssGridDimension> Dimension => _layout.Dimensions.ToEssGridDimension();
    }
}
