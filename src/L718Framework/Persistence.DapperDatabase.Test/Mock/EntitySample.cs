using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using L718Framework.Core.Domain.Model;
using Microsoft.CodeAnalysis.CSharp.Syntax;

public class EntitySample : IEntity<int>
{
    protected EntitySample()
    {
        this.Name = "";
        this.Id=0;
    }
    public EntitySample(int id,string name) 
    {
        this.Name = name;
        this.Id = id;
    }
    public string Name { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id {get;set;}

    public DateTime CreationDate {get;set;}
}