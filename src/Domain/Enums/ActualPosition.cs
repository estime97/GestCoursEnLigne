using System.ComponentModel;

namespace BlazorHero.CleanArchitecture.Domain.Enums
{
    public enum ActualPosition
    {
        [Description("GUICHET UNIQUE DU TITRE FONCIER")]
        GUICHET_UNIQUE,
        [Description("FORMALITE ET PREALABLE")]
        FORMALITE_PREALABLE,
        [Description("SECURISATION")]
        SECURISATION,
        [Description("TRAITEMENT ET AFFICHAGE")]
        TRAITEMENT_AFFICHAGE,
        [Description("ETUDE ET TRANSMISSION")]
        ETUDE_TRANSMISSION,
        [Description("BUREAU SAISIE DES BA")]
        SAISIE_BA,
        [Description("BUREAU LECTURE ET CONTRÔLE")]
        LECTURE_CONTROLE,
        [Description("BUREAU CREATION")]
        CREATION,
        [Description("BUREAU CONTENTIEUX")]
        CONTENTIEUX
    }
}
