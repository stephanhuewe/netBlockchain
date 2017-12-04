namespace Huestel.Blockchain.Example
{
    public class Block
    {
        public Block(BlockContent content)
        {
            Hash = Example.Hash.CreateHash(content.ToString());
            Content = content;
        }

        public string Hash { get; private set; }
        public BlockContent Content { get; set; }
    }
}
