using AppAngelaAbonos.Models;

namespace AppAngelaAbonos.ViewModels
{
    public  class DocuemntoDetalleViewModel : BaseViewModel
    {
           
        public Documento Documento { get; set; }
        public DocuemntoDetalleViewModel(Documento item = null)
        {
            Title = item?.Cliente.Nombre;
            Documento = item;
        }
    }
}
