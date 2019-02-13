using System;
using System.ComponentModel.DataAnnotations;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;

namespace eGandalf.Spellbook.ReductoBlock.Models
{
    [ContentType(DisplayName = "Reducto Block", GUID = "eaf14e5f-5940-4907-a474-f54ab8d6f2dd", Description = "Add to a ContentArea to eliminate all blocks prior to this block's position.")]
    public class ReductoBlock : BlockData
    {
        
    }
}