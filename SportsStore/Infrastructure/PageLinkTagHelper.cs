using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.Models.ViewModels;

namespace SportsStore.Infrastructure;

// This class is a TagHelper that generates a set of links to navigate between pages of a product list.
[HtmlTargetElement("div", Attributes = "page-model")]
public class PageLinkTagHelper : TagHelper
{
    private IUrlHelperFactory urlHelperFactory;

    public PageLinkTagHelper(IUrlHelperFactory helperFactory)
    {
        urlHelperFactory = helperFactory;
    }

    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; }

    public PagingInfo PageModel { get; set; }

    public string PageAction { get; set; }

    public override void Process(TagHelperContext tagHelperContext, TagHelperOutput tagHelperOutput)
    {
        if (ViewContext != null && PageModel.TotalPages > 1 && PageModel != null)
        {
            IUrlHelper? urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder? result = new("div");

            for (int i = 1; i <= PageModel.TotalPages; i++)
            {
                TagBuilder? tag = new("a");
                tag.Attributes["href"] = urlHelper.Action(PageAction, new { productPage = i });
                tag.InnerHtml.Append(i.ToString());
                result.InnerHtml.AppendHtml(tag);
            }

            tagHelperOutput.Content.AppendHtml(result);
        }
    }
}
