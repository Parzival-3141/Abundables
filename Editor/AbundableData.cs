using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abundables
{
    public class AbundableData
    {
        public Bundle[] bundles;

        public Bundle[] GetBundles() 
        {
            throw new NotImplementedException();
        }

        public void BuildBundle(Bundle bundle)
        {
            throw new NotImplementedException();
        }

        public void BuildBundles(Bundle[] bundles)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class Bundle
    {
        public string name;
        public Entry[] entries;

        [Serializable]
        public class Entry
        {
            public string assetPath;
            public string address;
        }
    }

}
