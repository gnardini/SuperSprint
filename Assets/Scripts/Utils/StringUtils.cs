using System;

public class StringUtils {

    public static string floatToTime(float secs){
        if (secs < 0) {
            return "--:--:--";
        }
        int min = (int)(secs / 60);
        int seg = (int)(secs - min * 60);
        int ms = (int)((secs - min * 60 - seg) * 100);
        return min.ToString("D2") + ":" + seg.ToString("D2") + ":" + ms.ToString("D2");
    }

}

