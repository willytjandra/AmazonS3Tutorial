namespace AmazonS3Tutorial.ConsoleApp.AmazonS3
{
    public class AwsConfig
    {
        public string BucketName { get; set; }
        public string RegionName { get; set; }
        public string AwsAccessKey { get; set; }
        public string AwsSecretKey { get; set; }
        public string LocalStackServiceUrl { get; set; }
    }
}
