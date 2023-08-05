

// 取得P-BaseDefenseGame中所使用的工廠
public static class Factorys
{
    private static readonly bool m_bLoadFromResource = true;
    private static IAssetFactory m_AssetFactory;

    // 取得將Unity Asset實作化的工廠
    public static IAssetFactory GetAssetFactory()
    {
        if (m_AssetFactory == null)
        {
            if (m_bLoadFromResource)
                //m_AssetFactory = new ResourceAssetFactory();
                m_AssetFactory = new ResourceAssetProxyFactory();
            else
                m_AssetFactory = null;
        }

        return m_AssetFactory;
    }
}