using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using System.Collections.Generic;

namespace AbstractAircraftFactoryLogic.BusinessLogics
{
	class PdfRowParameters
	{
		public Table Table { get; set; }
		public List<string> Texts { get; set; }
		public string Style { get; set; }
		public ParagraphAlignment ParagraphAlignment { get; set; }
	}
}
