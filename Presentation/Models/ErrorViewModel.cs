using System;

namespace Presentation.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
        public string Title { get; set; }
        public string Error { get; set; }
        public string Link { get; set; }
    }
}
