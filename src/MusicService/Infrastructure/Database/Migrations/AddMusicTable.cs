
using FluentMigrator;

namespace MusicPracticePlanner.MusicService.Infrastructure.Database.Migrations;

[Migration(1)]
public class AddMusicTable : Migration
{
    public override void Up()
    {
        Create.Table("Musics")
            .WithColumn("Id").AsString().PrimaryKey()
            .WithColumn("Name").AsString()
            .WithColumn("PdfFile").AsString()
            .WithColumn("CreationDate").AsDateTime();
    }
    public override void Down()
    {
        Delete.Table("Musics");
    }

}