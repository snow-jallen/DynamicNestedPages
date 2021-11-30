using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicNestedPages.Pages
{
    public class NestedDynamicPagesModel : PageModel
    {
        private readonly ILogger<NestedDynamicPagesModel> log;
        private readonly ItemManager itemManager;

        public NestedDynamicPagesModel(ILogger<NestedDynamicPagesModel> log, ItemManager itemManager)
        {
            this.log = log;
            this.itemManager = itemManager;
        }
        public void OnGet(string parent, string child)
        {
            ParentName = parent;
            ChildName = child;
        }

        [BindProperty]
        public string ParentName { get; set; }
        [BindProperty]
        public string ChildName { get; set; }

        public IActionResult OnPostAddParent()
        {
            log.LogInformation("Adding new parent: {parent}", ParentName);
            itemManager.TopLevelItems.Add(new ParentItem { Name = ParentName });
            return RedirectToPage();
        }

        public IActionResult OnPostAddChild()
        {
            log.LogInformation("Adding {child} as a child to {parent}", ChildName, ParentName);
            itemManager.TopLevelItems.First(i => i.Name == ParentName).Children.Add(ChildName);
            return RedirectToPage(new { parent = ParentName });
        }
    }
}
