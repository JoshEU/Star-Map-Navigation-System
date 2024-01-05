using UnityEngine;

// This Class consists of a library of 10 different Custom Eases. All of which contain an Ease In, Ease Out & Ease InOut with each having a return function as well by using polymorphism.
public class CustomEasing {
    // This class consists of Sine Eases #1
    public class Sine {
        public static float SineIn(float t) {
            return 1f - Mathf.Cos(t * Mathf.PI);
        }
        public static float SineIn(float t, bool b) {
            if (b) {
                return Mathf.PingPong(SineIn(t), 0.5f) * 2f;
            } else {
                return SineIn(t);
            }
        }
        public static float SineOut(float t) {
            return Mathf.Sin(t * Mathf.PI / 2);
        }
        public static float SineOut(float t, bool b) {
            if (b) {
                return Mathf.PingPong(SineOut(t), 0.5f) * 2f;
            } else {
                return SineOut(t);
            }
        }
        public static float SineInOut(float t) {
            return -(Mathf.Cos(Mathf.PI * t) - 1);
        }
        public static float SineInOut(float t, bool b) {
            if (b) {
                return Mathf.PingPong(SineInOut(t), 0.5f) * 2f;
            } else {
                return SineInOut(t);
            }
        }
    }
    // This class consists of Quadratic Eases #2
    public class Quadratic {
        public static float QuadIn(float t) {
            return t * t;
        }
        public static float QuadIn(float t, bool b) {
            if (b) {
                // Returns to original position if bool is true
                return Mathf.PingPong(QuadIn(t), 0.5f);
            } else {
                // Carries out normal EaseIn function without returning.
                return QuadIn(t);
            }
        }
        public static float QuadOut(float t) {
            return 1 - (1 - t) * (1 - t);
        }
        public static float QuadOut(float t, bool b) {
            if (b) {
                return Mathf.PingPong(QuadOut(t), 0.5f);
            } else {
                return QuadOut(t);
            }
        }
        public static float QuadInOut(float t) {
            return t < 0.5f
                ? 2 * t * t
                : 1 - Mathf.Pow(-2 * t + 2, 2) / 2;
        }
        public static float QuadInOut(float t, bool b) {
            if (b) {
                return Mathf.PingPong(QuadInOut(t), 0.5f);
            } else {
                return QuadInOut(t);
            }
        }
    }
    // This class consists of Cubic Eases #3
    public class Cubic {
        public static float CubicIn(float t) {
            return t * t * t;
        }
        public static float CubicIn(float t, bool b) {
            if (b) {
                return Mathf.PingPong(CubicIn(t), 0.5f);
            } else {
                return CubicIn(t);
            }
        }
        public static float CubicOut(float t) {
            return 1 - Mathf.Pow(1 - t, 3f);
        }
        public static float CubicOut(float t, bool b) {
            if (b) {
                return Mathf.PingPong(CubicOut(t), 0.5f);
            } else {
                return (CubicOut(t));
            }
        }
        public static float CubicInOut(float t) {
            return t < 0.5f
                ? 4 * CubicIn(t)
                : 1 - Mathf.Pow(-2f * t + 2, 3) / 2;
        }
        public static float CubicInOut(float t, bool b) {
            if (b) {
                return Mathf.PingPong(CubicInOut(t), 0.5f);
            } else {
                return CubicInOut(t);
            }

        }
    }
    // This class consists of Quaternion Eases #4
    public class Quaternion {
        public static float QuaterIn(float t) {
            return t * t * t * t;
        }
        public static float QuaterIn(float t, bool b) {
            if (b) {
                return Mathf.PingPong(QuaterIn(t), 0.5f);
            } else {
                return QuaterIn(t);
            }
        }
        public static float QuaterOut(float t) {
            return 1 - Mathf.Pow(1 - t, 1);
        }
        public static float QuaterOut(float t, bool b) {
            if (b) {
                return Mathf.PingPong(QuaterOut(t), 0.5f);
            } else {
                return (QuaterOut(t));
            }
        }
        public static float QuaterInOut(float t) {
            return t < 0.5f
                ? 8 * QuaterIn(t)
                : 1 - Mathf.Pow(-2 * t + 2, 4) / 2;
        }
        public static float QuaterInOut(float t, bool b) {
            if (b) {
                return Mathf.PingPong(QuaterInOut(t), 0.5f);
            } else {
                return (QuaterInOut(t));
            }
        }
    }
    // This class consists of Quintic Eases #5
    public class Quintic {
        public static float QuinticIn(float t) {
            return t * t * t * t * t;
        }
        public static float QuinticIn(float t, bool b) {
            if (b) {
                return Mathf.PingPong(QuinticIn(t), 0.5f);
            } else {
                return QuinticIn(t);
            }
        }
        public static float QuinticOut(float t) {
            return 1 - Mathf.Pow(1 - t, 5);
        }
        public static float QuinticOut(float t, bool b) {
            if (b) {
                return Mathf.PingPong(QuinticOut(t), 0.5f);
            } else {
                return QuinticOut(t);
            }
        }
        public static float QuinticInOut(float t) {
            return t < 0.5
                ? 16 * QuinticIn(t)
                : 1 - Mathf.Pow(-2 * t + 2, 5) / 2;
        }
        public static float QuinticInOut(float t, bool b) {
            if (b) {
                return Mathf.PingPong(QuinticInOut(t), 0.5f);
            } else {
                return QuinticInOut(t);
            }
        }
    }
    // This class consists of Exponential Eases #6
    public class Exponential {
        public static float ExponentialIn(float t) {
            return t == 0
                ? 0
                : Mathf.Pow(2, 10 * t - 10);
        }
        public static float ExponentialIn(float t, bool b) {
            if (b) {
                return Mathf.PingPong(ExponentialIn(t), 0.5f);
            } else {
                return ExponentialIn(t);
            }
        }
        public static float ExponentialOut(float t) {
            return t == 1
                ? 1
                : 1 - Mathf.Pow(2, -10 * t);
        }
        public static float ExponentialOut(float t, bool b) {
            if (b) {
                return Mathf.PingPong(ExponentialOut(t), 0.5f);
            } else {
                return (ExponentialOut(t));
            }
        }
        public static float ExponentialInOut(float t) {
            return t == 0
                ? 0
                : t == 1
                ? 1
                : t < 0.5
                ? Mathf.Pow(2, 20 * t - 10) / 2
                : (2 - Mathf.Pow(2, -20 * t + 10)) / 2;
        }
        public static float ExponentialInOut(float t, bool b) {
            if (b) {
                return Mathf.PingPong(ExponentialInOut(t), 0.5f);
            } else {
                return ExponentialInOut(t);
            }
        }
    }
    // This class consists of Circular Eases #7
    public class Circular {
        public static float CircularIn(float t) {
            return 1 - Mathf.Sqrt(1 - Mathf.Pow(t, 2));
        }
        public static float CircularIn(float t, bool b) {
            if (b) {
                return Mathf.PingPong(CircularIn(t), 0.5f);
            } else {
                return (CircularIn(t));
            }
        }
        public static float CircularOut(float t) {
            return Mathf.Sqrt(1 - Mathf.Pow(t - 1, 2));
        }
        public static float CircularOut(float t, bool b) {
            if (b) {
                return Mathf.PingPong(CircularOut(t), 0.5f);
            } else {
                return CircularOut(t);
            }
        }
        public static float CircularInOut(float t) {
            return t < 0.5f
                ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * t, 2))) / 2
                : (Mathf.Sqrt(1 - Mathf.Pow(-2 * t + 2, 2)) + 1) / 2;
        }
        public static float CircularInOut(float t, bool b) {
            if (b) {
                return Mathf.PingPong(CircularInOut(t), 0.5f);
            } else {
                return CircularInOut(t);
            }
        }
    }
    // This class consists of Back Eases #8
    public class Back {
        public static float BackIn(float t) {
            const float valueOne = 1.70158f;
            const float valueTwo = valueOne + 1;

            return valueTwo * t * t * t - valueOne * t * t;
        }
        public static float BackIn(float t, bool b) {
            if (b) {
                return Mathf.PingPong(BackIn(t), 0.5f);
            } else {
                return BackIn(t);
            }
        }
        public static float BackOut(float t) {
            const float valueOne = 1.70158f;
            const float valueTwo = valueOne + 1;

            return 1 + valueTwo * Mathf.Pow(t - 1, 3) + valueOne * Mathf.Pow(t - 1, 2);
        }
        public static float BackOut(float t, bool b) {
            if (b) {
                return Mathf.PingPong(BackOut(t), 0.5f);
            } else {
                return BackOut(t);
            }
        }
        public static float BackInOut(float t) {
            const float valueOne = 1.70158f;
            const float valueTwo = valueOne * 1.525f;

            return t < 0.5
                ? (Mathf.Pow(2 * t, 2) * ((valueTwo + 1) * 2 * t - valueTwo)) / 2
                : (Mathf.Pow(2 * t - 2, 2) * ((valueTwo + 1) * (t * 2 - 2) + valueTwo) + 2) / 2;
        }
        public static float BackInOut(float t, bool b) {
            if (b) {
                return Mathf.PingPong(BackInOut(t), 0.5f);
            } else {
                return BackInOut(t);
            }
        }
    }
    // This class consists of Elastic Eases #9
    public class Elastic {
        public static float ElasticIn(float t) {
            const float valueOne = (2 * Mathf.PI) / 3;

            return t == 0
                ? 0
                : t == 1
                ? 1
                : -Mathf.Pow(2, 10 * t - 10) * Mathf.Sin((t * 10 - 10.75f) * valueOne);
        }
        public static float ElasticIn(float t, bool b) {
            if (b) {
                return Mathf.PingPong(ElasticIn(t), 0.5f);
            } else {
                return (ElasticIn(t));
            }
        }
        public static float ElasticOut(float t) {
            const float valueOne = (2 * Mathf.PI) / 3;

            return t == 0
                ? 0
                : t == 1
                ? 1
                : Mathf.Pow(2, -10 * t) * Mathf.Sin((t * 10 - 0.75f) * valueOne) + 1;
        }
        public static float ElasticOut(float t, bool b) {
            if (b) {
                return Mathf.PingPong(ElasticOut(t), 0.5f);
            } else {
                return ElasticOut(t);
            }
        }
        public static float ElasticInOut(float t) {
            const float valueOne = (2 * Mathf.PI) / 4.5f;

            return t == 0
                ? 0
                : t == 1
                ? 1
                : t < 0.5f
                ? -(Mathf.Pow(2, 20 * t - 10) * Mathf.Sin((20 * t - 11.125f) * valueOne)) / 2
                : (Mathf.Pow(2, -20 * t + 10) * Mathf.Sin((20 * t - 11.125f) * valueOne)) / 2 + 1;
        }
        public static float ElasticInOut(float t, bool b) {
            if (b) {
                return Mathf.PingPong(ElasticInOut(t), 0.5f);
            } else {
                return ElasticInOut(t);
            }
        }
    }
    // This class consists of Bounce Eases #10
    public class Bounce {
        public static float BounceIn(float t) {
            return 1 - BounceOut(1 - t);
        }
        public static float BounceIn(float t, bool b) {
            if (b) {
                return Mathf.PingPong(BounceIn(t), 0.5f);
            } else {
                return BounceIn(t);
            }
        }
        public static float BounceOut(float t) {
            const float valueOne = 7.5625f;
            const float valueTwo = 2.75f;

            if (t < 1 / valueTwo) {
                return valueOne * t * t;
            } else if (t < 2 / valueTwo) {
                return valueOne * (t -= 1.5f / valueTwo) * t + 0.75f;
            } else if (t < 2.5f / valueTwo) {
                return valueOne * (t -= 2.25f / valueTwo) * t + 0.9375f;
            } else {
                return valueOne * (t -= 2.625f / valueTwo) * t + 0.984375f;
            }
        }
        public static float BounceOut(float t, bool b) {
            if (b) {
                return Mathf.PingPong(BounceOut(t), 0.5f);
            } else {
                return BounceOut(t);
            }
        }
        public static float BounceInOut(float t) {
            return t < 0.5f
                ? (1 - BounceOut(1 - 2 * t)) / 2
                : (1 + BounceOut(2 * t - 1)) / 2;
        }
        public static float BounceInOut(float t, bool b) {
            if (b) {
                return Mathf.PingPong(BounceInOut(t), 0.5f);
            } else {
                return BounceInOut(t);
            }
        }
    }
}