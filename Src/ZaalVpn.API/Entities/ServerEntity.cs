namespace ZaalVpn.API.Entities
{
    public class ServerEntity : BaseEntity
    {
        public ServerEntity(string countryId, List<ConfigEntity> configs, bool isActive)
        {
            CountryId = countryId;
            Configs = configs;
            IsActive = isActive;
        }


        public CountryEntity Country { get; private set; }
        public string CountryId { get; private set; }

        public List<ConfigEntity> Configs { get; private set; }

        public bool IsActive { get; private set; }

        public void Edit(string countryId, bool isActive)
        {
            CountryId = countryId;
            IsActive = isActive;
        }
    }

    public class ConfigEntity : BaseEntity
    {
        public ConfigEntity(string config, bool isActive, string serverId)
        {
            Config = config;
            IsActive = isActive;
            ServerId = serverId;
        }

        public string Config { get; private set; }
        public bool IsActive { get; private set; }

        public ServerEntity Server { get; set; }
        public string ServerId { get; set; }


        public void Edit(string config, bool isActive, string serverId)
        {
            Config = config;
            IsActive = isActive;
            ServerId = serverId;
        }
    }
    public class CountryEntity : BaseEntity
    {
        public CountryEntity(string name)
        {
            Name = name;
        }

        public void Edit(string name)
        {
            Name = name;
        }

        public List<ServerEntity> Servers { get; private set; }
        public string Name { get; private set; }
    }


    public class BaseEntity
    {
        public string Id { get; private set; }
        public DateTime CreateTimeAt { get; private set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString("N");
            CreateTimeAt = DateTime.Now;
        }
    }

}
