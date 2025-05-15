using BlazorHero.CleanArchitecture.Domain.Contracts;

namespace BlazorHero.CleanArchitecture.Domain.Entities.SuiviRequisition
{
    public class Requisition : AuditableEntity<int>
    {
        public string NumeroRequisition { get; set; }
        public string NomRequerant { get; set; }
        public string PrenomRequerant { get; set; }
        public string Localite { get; set; }
        public string DateRequisition { get; set; }
        public string BureauRequisition { get; set; }
        public string? Geometre { get; set; }
        public string? DateBornage { get; set; }
        public string? EquipeBornage { get; set; }
        public string? DateTransmission { get; set; }
        public string TypePrestation { get; set; }
        public string? NumeroJORT { get; set; }
        public string? DateInsertionJORT { get; set; }
        public string? DateAffichage { get; set; }
        public string? DatePublication { get; set; }
        public string? NumeroTitre { get; set; }
        public string? DateSigned { get; set; }
        public string? DateRetrait { get; set; }
        public string StatutRequisition { get; set; }
        public string Region { get; set; }
        public string? MotifRejet { get; set; }
    }
}
