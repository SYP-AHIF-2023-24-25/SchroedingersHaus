package helper;

import java.util.Locale;
import java.util.Random;

//Quelle: https://stackoverflow.com/questions/41107/how-to-generate-a-random-alpha-numeric-string
public class RandomStringGenerate {

    public static final String upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    public static final String lower = upper.toLowerCase(Locale.ROOT);

    public static final String digits = "0123456789";

    public static final String alphanum = upper + lower + digits;

    private final Random random = new Random();
    private final char[] symbols = alphanum.toCharArray();

    /**
     * Generate a random string.
     */
    public String GenerateRandomString(int length) {
        char[] buf = new char[length];

        for (int idx = 0; idx < buf.length; ++idx)
            buf[idx] = symbols[random.nextInt(symbols.length)];
        return new String(buf);
    }
}
