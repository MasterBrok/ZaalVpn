namespace ZaalVpn.API.Entities;

/// <summary>
/// 
/// </summary>
public class GenderEntity : BaseEntity
{
    protected GenderEntity(){}
    public GenderEntity(string gender)
    {
        Gender = gender;
    }


    public string Gender { get; private set; }

    //Male =1 ,
    //Female = 2 ,
    //NonBinary =3 ,
    //Bigender = 3 ,
    //Agender = 4,
    //Feminine = 5 , 
    //Androgynous = 6,
    //Other = 7,
    public void Edit(string gender)
    {
        Gender = gender;
    }
}