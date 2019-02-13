using eGandalf.Spellbook.ReductoBlock.Models;
using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using EPiServer.Web.Routing;
using System;
using System.Linq;
using System.Web.Mvc;

namespace eGandalf.Spellbook.ReductoBlock.Infrastructure
{
    internal class BlockSpells
    {
        private IContentRepository _contentRepository;
        private IPageRouteHelper _pageRouteHelper;

        internal BlockSpells(IContentRepository contentRepository, IPageRouteHelper pageRouteHelper)
        {
            _contentRepository = contentRepository ?? throw new ArgumentNullException(nameof(contentRepository));
            _pageRouteHelper = pageRouteHelper ?? throw new ArgumentNullException(nameof(pageRouteHelper));
        }

        internal BlockSpells() :
            this(ServiceLocator.Current.GetInstance<IContentRepository>(),
                ServiceLocator.Current.GetInstance<IPageRouteHelper>()) { }


        internal int GetCurrentBlockIndex(ContentArea contentArea, Models.ReductoBlock currentBlock)
        {
            return contentArea.Items.ToList().FindIndex(o => o.ContentGuid == ((IContent)currentBlock).ContentGuid);
        }

        internal ContentArea GetBlockArea(ControllerContext controllerContext)
        {
            return controllerContext.ParentActionViewContext.ViewData.Model as ContentArea;
        }

        internal void CastReductoBlockSpell(SpellParams model, ContentArea contentArea)
        {
            var currentPage = _pageRouteHelper.Page;
            var editableContent = _contentRepository.Get<PageData>(currentPage.ContentLink).CreateWritableClone();
            foreach (var property in currentPage.Property)
            {
                if (property.PropertyValueType != typeof(ContentArea) || property.Value?.Equals(contentArea) != true)
                {
                    continue;
                }
                for (int i = 0; i <= model.Index; i++)
                {
                    ((ContentArea)editableContent.Property[property.Name].Value).Items.RemoveAt(0);
                }
                _contentRepository.Save(editableContent, EPiServer.DataAccess.SaveAction.CheckOut, EPiServer.Security.AccessLevel.Edit);
            }
        }
    }
}
