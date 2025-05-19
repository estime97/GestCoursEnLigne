using System.ComponentModel;

namespace BlazorHero.CleanArchitecture.Domain.Enums
{
    public enum Statut
    {
        [Description("Rejetée")]
        Rejected,
        [Description("Annulée")]
        Cancelled,
        [Description("Créée")]
        Created,
        [Description("Formalité Préalable")]
        FormalitePrealable,
        [Description("Envoie Editeur")]
        EnvoieEditeur,
        [Description("Retour Editeur")]
        RetourEditeur,
        [Description("Programmation Bornage")]
        ProgrammationBornage,
        [Description("Affichage")]
        Affichage,
        [Description("Bornage")]
        Bornage,
        [Description("Sécurisée")]
        SecurisationFonciere,
        [Description("Plan Validation")]
        PlanValidation,
        [Description("Bordereau Analytique")]
        BorderauAnalytique,
        [Description("Validation Bordereau")]
        ValidationBordereau,
        [Description("Opposition")]
        Opposition,
        [Description("Immatriculation")]
        Immatriculation,
        [Description("Conservation")]
        Conservation,
        [Description("Contentieux")]
        Contentieux,
        [Description("Bornage Nul")]
        BornageNul,
        [Description("En attente de traitement")]
        EnAttenteTraitement,
        [Description("Rejeté et en instance d'envoie au cadastre")]
        RejeteInstanceEnvoieCadastre,
        [Description("Rejeté et en instance de transmission")]
        RejeteInstanceTransmission,
        [Description("Rejeté et en instance de retour au contentieux")]
        RejeteInstanceRetourContentieux,
        [Description("Rejeté et en instance de régularisation")]
        RejeteInstanceRegularisation,
        [Description("Bloqué")]
        Bloque,
        [Description("Bornage Executé")]
        BornageExecute,
        [Description("Publication au JO")]
        PublicationJO,
        [Description("Plan Rejeté")]
        PlanRejete,
        [Description("Plan Validé")]
        PlanValide,
        [Description("Main Levée")]
        MainLevee
    }
}
