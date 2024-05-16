using System.Text.Json.Serialization;

namespace SimemNetAdmin.Domain.Models.Dcat
{
    public class DcatJsonModel
    {
        public string conformsTo { get; set; } = "https://project-open-data.cio.gov/v1.1/schema";
        public string describedBy { get; set; } = "https://project-open-data.cio.gov/v1.1/schema";
        [JsonPropertyName("@context")]
        public string context { get; set; } = "https://project-open-data.cio.gov/v1.1/schema";
        [JsonPropertyName("@type")]
        public string Type { get; set; } = "dcat:Catalog";
        public List<DataSet> DataSet { get; set; } = [];
}

    public class DataSet {
        [JsonPropertyName("@type")]
        public string Type { get; set; } = "dcat:Dataset";
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Keyword { get; set; } = [];
        public string Modified { get; set; } = string.Empty;
        public Publisher Publisher { get; set; } = new Publisher();
        public ContactPoint ContactPoint { get; set; } = new ContactPoint();
        public string Identifier { get; set; } = string.Empty;
        public string AccessLevel { get; set; } = string.Empty;
    }

    public class ContactPoint
    {
        [JsonPropertyName("@type")]
        public string Type { get; set; } = "vcard:Contact";
        public string Fn { get; set; } = "Equipo Simem";
        public string HasEmail { get; set; } = "mailto:equipo_SIMEM@XM.com.co";
    }

    public class Publisher
    {
        [JsonPropertyName("@type")]
        public string Type { get; set; } = "org:Organization";
        public string Name { get; set; } = "XM";
        public SubOrganizacion SubOrganizationOf { get; set; } = new();
    }

    public class SubOrganizacion
    {
        [JsonPropertyName("@type")]
        public string Type { get; set; } = "org:Organization";
        public string Name { get; set; } = "Simem";
    }
}
