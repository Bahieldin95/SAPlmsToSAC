using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Converters;

public class Student
{
   
    public string? STUDID { get; set; }

    public string? LASTNAME { get; set; }

    public string? FIRSTNAME { get; set; }

    public string? ARABIC_NAME { get; set; }

    public string? JOB_TITLE { get; set; }

    public string? ORG_ID { get; set; }

    public string? ACTIVE { get; set; }

    public string? ADDR { get; set; }

    public string? CITY { get; set; }

    public string? STAT { get; set; }

    public string? CNTRY { get; set; }

    public string? EMAIL { get; set; }

    public string? COMMENTS { get; set; }

    public string? GENDER { get; set; }

    public String? CPNTTYPEID { get; set; }  // Nullable int

    public String? CPNTID { get; set; }  // Nullable int

    [Newtonsoft.Json.JsonProperty("REVDATE", ItemConverterType = typeof(IsoDateTimeConverter))]
    public String? REVDATE { get; set; }  // Nullable DateTime

    public String? SCHEDID { get; set; }  // Nullable int

    public string? CPNTDESC { get; set; }

    public String? GRADE { get; set; }  // Nullable int

    [Key]
    public String? PRIMKEY { get; set; }  // Nullable int

    [Newtonsoft.Json.JsonProperty("COMPLDATE", ItemConverterType = typeof(IsoDateTimeConverter))]
    public String? COMPLDATE { get; set; }  // Nullable DateTime

    public string? COMPLSTATID { get; set; }

    public string? COMPLSTATDESC { get; set; }

    public String? TOTALHRS { get; set; }  // Nullable int

    public String? CREDITHRS { get; set; }  // Nullable int

    public String? CONTACTHRS { get; set; }  // Nullable int

    public String? CPEHRS { get; set; }  // Nullable int

    public String? TUITION { get; set; }  

    public string? INSTNAME { get; set; }

    public string? EVENTCOMMENTS { get; set; }

    [Newtonsoft.Json.JsonProperty("LSTTMSP", ItemConverterType = typeof(IsoDateTimeConverter))]
    public String? LSTTMSP { get; set; }  // Nullable DateTime

    public string? CPNTCLASSIFICATION { get; set; }
}
