namespace Chat.Infra.Migrations.Seeds
{
    public interface ISeed<T>
    {
        T[] Seed();
    }
}