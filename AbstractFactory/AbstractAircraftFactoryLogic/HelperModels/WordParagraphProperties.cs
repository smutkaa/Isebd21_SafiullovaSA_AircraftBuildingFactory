using DocumentFormat.OpenXml.Wordprocessing;

namespace AbstractAircraftFactoryLogic.HelperModels
{
	class WordParagraphProperties
	{
		public string Size { get; set; }
		public bool Bold { get; set; }
		public JustificationValues JustificationValues { get; set; }
	}
}
