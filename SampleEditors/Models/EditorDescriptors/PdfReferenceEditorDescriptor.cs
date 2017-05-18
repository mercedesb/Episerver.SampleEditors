using EPiServer.Cms.Shell.UI.UIDescriptors;
using EPiServer.Core;
using EPiServer.Shell.ObjectEditing.EditorDescriptors;
using SampleEditors.Models.Media;

namespace SampleEditors.Models.EditorDescriptors
{
    [EditorDescriptorRegistration(TargetType = typeof(ContentReference), UIHint = MakingWavesUIHint.PDF)]
    public class PdfReferenceEditorDescriptor : ContentReferenceEditorDescriptor<SitePDFData>
    {
        public override string RepositoryKey
        {
            get
            {
                return MediaRepositoryDescriptor.RepositoryKey;
            }
        }

        public PdfReferenceEditorDescriptor()
        {
        }
    }
}
