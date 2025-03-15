namespace ZaalVpn.API.Entities
{
    public class ServerEntity : BaseEntity
    {
        protected ServerEntity(){}
        public ServerEntity(string countryId, bool isActive)
        {
            CountryId = countryId;
            IsActive = isActive;
        }


        public CountryEntity Country { get; private set; }
        public string CountryId { get; private set; }

        // Image

        public List<ConfigEntity>? Configs { get; private set; }

        public bool IsActive { get; private set; }

        public void Edit(string countryId, bool isActive)
        {
            CountryId = countryId;
            IsActive = isActive;
        }

        public void AddRangeConfigs(List<ConfigEntity> configs)
        {
            Configs = configs;
        }


    }

    public class ConfigEntity : BaseEntity
    {
        protected ConfigEntity(){}
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
        protected CountryEntity(){}
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
