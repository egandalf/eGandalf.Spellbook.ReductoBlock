using eGandalf.Spellbook.ReductoBlock.Infrastructure;
using eGandalf.Spellbook.ReductoBlock.Models;
using EPiServer.Framework.DataAnnotations;
using EPiServer.Web.Mvc;
using System.Web.Mvc;

namespace eGandalf.Spellbook.ReductoBlock.Controllers
{
    [TemplateDescriptor(Default = true, ModelType = typeof(Models.ReductoBlock))]
    public class ReductoBlockController : BlockController<Models.ReductoBlock>
    {
        public override ActionResult Index(Models.ReductoBlock currentContent)
        {
            var spellParams = new SpellParams();
            var spellCaster = new BlockSpells();

            var contentArea = spellCaster.GetBlockArea(ControllerContext);
            spellParams.Count = contentArea.Items.Count;
            spellParams.Index = spellCaster.GetCurrentBlockIndex(contentArea, currentContent);

            // only cast spell when there are blocks to destroy and they are in front of you
            if (spellParams.Count <= 1 || spellParams.Index < 1)
            {
                return PartialView("~/EPiServer/eGandalf.Spellbook.ReductoBlock/Views/ReductoBlock/index.cshtml", currentContent);
            }

            spellCaster.CastReductoBlockSpell(spellParams, contentArea);
            return PartialView("~/EPiServer/eGandalf.Spellbook.ReductoBlock/Views/ReductoBlock/index.cshtml", currentContent);
        }
    }
}
