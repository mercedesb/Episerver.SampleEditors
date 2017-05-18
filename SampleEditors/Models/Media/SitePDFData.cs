using EPiServer.Core;
using EPiServer.DataAnnotations;
using EPiServer.Framework.DataAnnotations;

namespace SampleEditors.Models.Media
{
    [ContentType(
        DisplayName = "PDF",
        GUID = "1814C9EB-236C-4FD3-9F66-07D6FE7A1DFF")]
    [MediaDescriptor(ExtensionString = "pdf")]
    public class SitePDFData : MediaData { }
}
