// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("772Fs/iYfNv0u2/3SV2pJYdjeu3YpnHUCiDyKqtMdc9gpvL+8e7WrdmaVO58p4c6Yrx4ic//pkZVRZxFSyUXXNej8Lj/pNMKqnL7VtnR63wGzxOKdFN6In3iqghPO0s6Rzg4zcdESkV1x0RPR8dEREWQbaG19HG2w2cNzeanKt5R4BlZjI2Ci8DoZHPeYVVkaR78E5JM95iYn3fCKP0lBhKM2IxNVWChX7TpEKyt9SELr+eC+We/rqKe/MwJXh6RYfkyhvn/tfLWcaW2WvyCZ/U0Y7hHvDMYp9dzTrgn7lqfMRIA+13hXFtrsyMfIbjPdcdEZ3VIQ0xvww3DskhERERARUa9QPEEkgnvJCFhHjbiw5EsDkwA9t6RJA/E5sQIqEdGREVE");
        private static int[] order = new int[] { 13,12,8,5,8,12,7,9,11,9,11,11,13,13,14 };
        private static int key = 69;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
