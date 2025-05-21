using BlazorHero.CleanArchitecture.Domain.Contracts;

namespace BlazorHero.CleanArchitecture.Domain.Entities.SuiviRequisition
{
    public class Requisition : AuditableEntity<int>
    {
        public string NumeroRequisition { get; set; }
        public string? NumeroTitreFoncier { get; set; }
        public string DateRequisition { get; set; }
        public string NomRequerant { get; set; }
        public string PrenomRequerant { get; set; }
        public string Bureau { get; set; }
        public string Region { get; set; }
        public string Statut { get; set; }
        public string? MotifRejet { get; set; }
        public string? PiecesManquantes { get; set; }
        public string? DateCreation { get; set; }
    }
}
