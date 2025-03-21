using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Book{
    
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int? Id { get; set;}
    public string? Title { get; set;}
    public string? Author { get; set;}
    public string? Description { get; set;}
    public string? Category { get; set;}
    public string? Publishing { get; set;}
    public string? Binding { get; set;}
    public int? Year { get; set;}
    public int? AgeLimit { get; set;}
    public double? Price { get; set;}
    public string? BookImage { get; set;}
}