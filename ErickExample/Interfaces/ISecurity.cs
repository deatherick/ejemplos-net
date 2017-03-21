namespace ErickExample.Interfaces
{
    interface ISecurity
    {
        string sha256_hash(string value);
        string md5_hash(string value);
    }
}
