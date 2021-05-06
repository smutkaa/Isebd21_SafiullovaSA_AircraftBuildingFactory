using AbstractAircraftFactoryLogic.Attributes;
using System.ComponentModel;

namespace AbstractAircraftFactoryLogic.ViewModels
{
    public class ComponentViewModel
    {
    public int Id { get; set; }
    [Column(title: "Название компонента", gridViewAutoSize: GridViewAutoSize.Fill)]
    [DisplayName("Название компонента")]
     public string ComponentName { get; set; }
    }
}
