using System.Xml.Serialization;

namespace EstadoDeVistasYTramites.Model
{
    public class Vista
    {
        public string SUBNOMBRE { get; set; }

        public string VISDETALLE { get; set; }

        [XmlElement("TO_CHAR_x0028_VISFECHACT_x002C__x0027_DD--MM--RRRR_x0027__x0029")]
        public string TO_CHAR_x0028_VISFECHACT_x002C__x0027_DD_MM_RRRR_x0027__x0029_ { get; set; }

        [XmlElement("TO_CHAR_x0028_VISFECHART_x002C__x0027_DD__MM__RRRR_x0027__x0029_--MM--RRRR_x0027__x0029")]
        public string TO_CHAR_x0028_VISFECHART_x002C__x0027_DD_MM_RRRR_x0027__x0029_ { get; set; }
    }
}
