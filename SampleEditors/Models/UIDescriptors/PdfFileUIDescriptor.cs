using EPiServer.Core;
using EPiServer.Shell;
using EPiServer.Shell.Services.Rest;
using SampleEditors.Models.Media;
using System;
using System.Collections.Generic;

namespace SampleEditors.Models.UIDescriptors
{
    [UIDescriptorRegistration]
    public class PdfFileUIDescriptor : UIDescriptor<SitePDFData>, IEditorDropBehavior
    {
        public bool ActAsAnAsset
        {
            get
            {
                return true;
            }
        }

        public EditorDropBehavior EditorDropBehaviour
        {
            get;
            set;
        }

        public PdfFileUIDescriptor()
        {
            base.IsPrimaryType = true;
            base.ContainerTypes = (IEnumerable<Type>)(new Type[] { typeof(ContentFolder) });
            base.SortKey = new SortColumn()
            {
                ColumnName = "typeIdentifier"
            };
            base.DefaultView = "formedit";
            base.AddDisabledView("onpageedit");
            this.EditorDropBehaviour = EditorDropBehavior.CreateLink;
        }
    }
}
