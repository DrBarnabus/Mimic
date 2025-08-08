namespace Mimic.UnitTests.Extensions;

public static class ReturnsExtensionsTests
{
    public static class TaskOfT
    {
        [Theory, AutoData]
        public static async Task Returns_WithNoParamsAndValue_ShouldReturnValue(int value)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.Task())
                .Returns(value);

            (await mimic.Object.Task()).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithNoParamsAndValueFunction_ShouldReturnValue(int value)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.Task())
                .Returns(() => value);

            (await mimic.Object.Task()).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithOneParamAndValueFunction_ShouldReturnValue(int value, int t)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.Task(t))
                .Returns((int a) =>
                {
                    a.ShouldBe(t);
                    return value;
                });

            (await mimic.Object.Task(t)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithTwoParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.Task(t1, t2))
                .Returns((int a1, int a2) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    return value;
                });

            (await mimic.Object.Task(t1, t2)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithThreeParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.Task(t1, t2, t3))
                .Returns((int a1, int a2, int a3) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    return value;
                });

            (await mimic.Object.Task(t1, t2, t3)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithFourParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3, int t4)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.Task(t1, t2, t3, t4))
                .Returns((int a1, int a2, int a3, int a4) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    a4.ShouldBe(t4);
                    return value;
                });

            (await mimic.Object.Task(t1, t2, t3, t4)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithFiveParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3, int t4, int t5)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.Task(t1, t2, t3, t4, t5))
                .Returns((int a1, int a2, int a3, int a4, int a5) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    a4.ShouldBe(t4);
                    a5.ShouldBe(t5);
                    return value;
                });

            (await mimic.Object.Task(t1, t2, t3, t4, t5)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithSixParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3, int t4, int t5, int t6)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.Task(t1, t2, t3, t4, t5, t6))
                .Returns((int a1, int a2, int a3, int a4, int a5, int a6) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    a4.ShouldBe(t4);
                    a5.ShouldBe(t5);
                    a6.ShouldBe(t6);
                    return value;
                });

            (await mimic.Object.Task(t1, t2, t3, t4, t5, t6)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithSevenParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3, int t4, int t5, int t6, int t7)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.Task(t1, t2, t3, t4, t5, t6, t7))
                .Returns((int a1, int a2, int a3, int a4, int a5, int a6, int a7) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    a4.ShouldBe(t4);
                    a5.ShouldBe(t5);
                    a6.ShouldBe(t6);
                    a7.ShouldBe(t7);
                    return value;
                });

            (await mimic.Object.Task(t1, t2, t3, t4, t5, t6, t7)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithEightParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.Task(t1, t2, t3, t4, t5, t6, t7, t8))
                .Returns((int a1, int a2, int a3, int a4, int a5, int a6, int a7, int a8) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    a4.ShouldBe(t4);
                    a5.ShouldBe(t5);
                    a6.ShouldBe(t6);
                    a7.ShouldBe(t7);
                    a8.ShouldBe(t8);
                    return value;
                });

            (await mimic.Object.Task(t1, t2, t3, t4, t5, t6, t7, t8)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithNineParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.Task(t1, t2, t3, t4, t5, t6, t7, t8, t9))
                .Returns((int a1, int a2, int a3, int a4, int a5, int a6, int a7, int a8, int a9) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    a4.ShouldBe(t4);
                    a5.ShouldBe(t5);
                    a6.ShouldBe(t6);
                    a7.ShouldBe(t7);
                    a8.ShouldBe(t8);
                    a9.ShouldBe(t9);
                    return value;
                });

            (await mimic.Object.Task(t1, t2, t3, t4, t5, t6, t7, t8, t9)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithTenParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.Task(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10))
                .Returns((int a1, int a2, int a3, int a4, int a5, int a6, int a7, int a8, int a9, int a10) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    a4.ShouldBe(t4);
                    a5.ShouldBe(t5);
                    a6.ShouldBe(t6);
                    a7.ShouldBe(t7);
                    a8.ShouldBe(t8);
                    a9.ShouldBe(t9);
                    a10.ShouldBe(t10);
                    return value;
                });

            (await mimic.Object.Task(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithElevenParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.Task(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11))
                .Returns((int a1, int a2, int a3, int a4, int a5, int a6, int a7, int a8, int a9, int a10, int a11) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    a4.ShouldBe(t4);
                    a5.ShouldBe(t5);
                    a6.ShouldBe(t6);
                    a7.ShouldBe(t7);
                    a8.ShouldBe(t8);
                    a9.ShouldBe(t9);
                    a10.ShouldBe(t10);
                    a11.ShouldBe(t11);
                    return value;
                });

            (await mimic.Object.Task(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithTwelveParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.Task(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12))
                .Returns((int a1, int a2, int a3, int a4, int a5, int a6, int a7, int a8, int a9, int a10, int a11, int a12) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    a4.ShouldBe(t4);
                    a5.ShouldBe(t5);
                    a6.ShouldBe(t6);
                    a7.ShouldBe(t7);
                    a8.ShouldBe(t8);
                    a9.ShouldBe(t9);
                    a10.ShouldBe(t10);
                    a11.ShouldBe(t11);
                    a12.ShouldBe(t12);
                    return value;
                });

            (await mimic.Object.Task(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithThirteenParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.Task(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13))
                .Returns((int a1, int a2, int a3, int a4, int a5, int a6, int a7, int a8, int a9, int a10, int a11, int a12, int a13) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    a4.ShouldBe(t4);
                    a5.ShouldBe(t5);
                    a6.ShouldBe(t6);
                    a7.ShouldBe(t7);
                    a8.ShouldBe(t8);
                    a9.ShouldBe(t9);
                    a10.ShouldBe(t10);
                    a11.ShouldBe(t11);
                    a12.ShouldBe(t12);
                    a13.ShouldBe(t13);
                    return value;
                });

            (await mimic.Object.Task(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithFourteenParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.Task(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14))
                .Returns((int a1, int a2, int a3, int a4, int a5, int a6, int a7, int a8, int a9, int a10, int a11, int a12, int a13, int a14) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    a4.ShouldBe(t4);
                    a5.ShouldBe(t5);
                    a6.ShouldBe(t6);
                    a7.ShouldBe(t7);
                    a8.ShouldBe(t8);
                    a9.ShouldBe(t9);
                    a10.ShouldBe(t10);
                    a11.ShouldBe(t11);
                    a12.ShouldBe(t12);
                    a13.ShouldBe(t13);
                    a14.ShouldBe(t14);
                    return value;
                });

            (await mimic.Object.Task(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithFifteenParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14, int t15)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.Task(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15))
                .Returns((int a1, int a2, int a3, int a4, int a5, int a6, int a7, int a8, int a9, int a10, int a11, int a12, int a13, int a14, int a15) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    a4.ShouldBe(t4);
                    a5.ShouldBe(t5);
                    a6.ShouldBe(t6);
                    a7.ShouldBe(t7);
                    a8.ShouldBe(t8);
                    a9.ShouldBe(t9);
                    a10.ShouldBe(t10);
                    a11.ShouldBe(t11);
                    a12.ShouldBe(t12);
                    a13.ShouldBe(t13);
                    a14.ShouldBe(t14);
                    a15.ShouldBe(t15);
                    return value;
                });

            (await mimic.Object.Task(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithSixteenParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14, int t15, int t16)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.Task(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16))
                .Returns((int a1, int a2, int a3, int a4, int a5, int a6, int a7, int a8, int a9, int a10, int a11, int a12, int a13, int a14, int a15, int a16) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    a4.ShouldBe(t4);
                    a5.ShouldBe(t5);
                    a6.ShouldBe(t6);
                    a7.ShouldBe(t7);
                    a8.ShouldBe(t8);
                    a9.ShouldBe(t9);
                    a10.ShouldBe(t10);
                    a11.ShouldBe(t11);
                    a12.ShouldBe(t12);
                    a13.ShouldBe(t13);
                    a14.ShouldBe(t14);
                    a15.ShouldBe(t15);
                    a16.ShouldBe(t16);
                    return value;
                });

            (await mimic.Object.Task(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16)).ShouldBe(value);
        }
    }

    public static class ValueTaskOfT
    {
        [Theory, AutoData]
        public static async Task Returns_WithNoParamsAndValue_ShouldReturnValue(int value)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.ValueTask())
                .Returns(value);

            (await mimic.Object.ValueTask()).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithNoParamsAndValueFunction_ShouldReturnValue(int value)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.ValueTask())
                .Returns(() => value);

            (await mimic.Object.ValueTask()).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithOneParamAndValueFunction_ShouldReturnValue(int value, int t)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.ValueTask(t))
                .Returns((int a) =>
                {
                    a.ShouldBe(t);
                    return value;
                });

            (await mimic.Object.ValueTask(t)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithTwoParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.ValueTask(t1, t2))
                .Returns((int a1, int a2) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    return value;
                });

            (await mimic.Object.ValueTask(t1, t2)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithThreeParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.ValueTask(t1, t2, t3))
                .Returns((int a1, int a2, int a3) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    return value;
                });

            (await mimic.Object.ValueTask(t1, t2, t3)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithFourParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3, int t4)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.ValueTask(t1, t2, t3, t4))
                .Returns((int a1, int a2, int a3, int a4) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    a4.ShouldBe(t4);
                    return value;
                });

            (await mimic.Object.ValueTask(t1, t2, t3, t4)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithFiveParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3, int t4, int t5)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.ValueTask(t1, t2, t3, t4, t5))
                .Returns((int a1, int a2, int a3, int a4, int a5) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    a4.ShouldBe(t4);
                    a5.ShouldBe(t5);
                    return value;
                });

            (await mimic.Object.ValueTask(t1, t2, t3, t4, t5)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithSixParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3, int t4, int t5, int t6)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.ValueTask(t1, t2, t3, t4, t5, t6))
                .Returns((int a1, int a2, int a3, int a4, int a5, int a6) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    a4.ShouldBe(t4);
                    a5.ShouldBe(t5);
                    a6.ShouldBe(t6);
                    return value;
                });

            (await mimic.Object.ValueTask(t1, t2, t3, t4, t5, t6)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithSevenParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3, int t4, int t5, int t6, int t7)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.ValueTask(t1, t2, t3, t4, t5, t6, t7))
                .Returns((int a1, int a2, int a3, int a4, int a5, int a6, int a7) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    a4.ShouldBe(t4);
                    a5.ShouldBe(t5);
                    a6.ShouldBe(t6);
                    a7.ShouldBe(t7);
                    return value;
                });

            (await mimic.Object.ValueTask(t1, t2, t3, t4, t5, t6, t7)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithEightParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.ValueTask(t1, t2, t3, t4, t5, t6, t7, t8))
                .Returns((int a1, int a2, int a3, int a4, int a5, int a6, int a7, int a8) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    a4.ShouldBe(t4);
                    a5.ShouldBe(t5);
                    a6.ShouldBe(t6);
                    a7.ShouldBe(t7);
                    a8.ShouldBe(t8);
                    return value;
                });

            (await mimic.Object.ValueTask(t1, t2, t3, t4, t5, t6, t7, t8)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithNineParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.ValueTask(t1, t2, t3, t4, t5, t6, t7, t8, t9))
                .Returns((int a1, int a2, int a3, int a4, int a5, int a6, int a7, int a8, int a9) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    a4.ShouldBe(t4);
                    a5.ShouldBe(t5);
                    a6.ShouldBe(t6);
                    a7.ShouldBe(t7);
                    a8.ShouldBe(t8);
                    a9.ShouldBe(t9);
                    return value;
                });

            (await mimic.Object.ValueTask(t1, t2, t3, t4, t5, t6, t7, t8, t9)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithTenParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.ValueTask(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10))
                .Returns((int a1, int a2, int a3, int a4, int a5, int a6, int a7, int a8, int a9, int a10) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    a4.ShouldBe(t4);
                    a5.ShouldBe(t5);
                    a6.ShouldBe(t6);
                    a7.ShouldBe(t7);
                    a8.ShouldBe(t8);
                    a9.ShouldBe(t9);
                    a10.ShouldBe(t10);
                    return value;
                });

            (await mimic.Object.ValueTask(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithElevenParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.ValueTask(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11))
                .Returns((int a1, int a2, int a3, int a4, int a5, int a6, int a7, int a8, int a9, int a10, int a11) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    a4.ShouldBe(t4);
                    a5.ShouldBe(t5);
                    a6.ShouldBe(t6);
                    a7.ShouldBe(t7);
                    a8.ShouldBe(t8);
                    a9.ShouldBe(t9);
                    a10.ShouldBe(t10);
                    a11.ShouldBe(t11);
                    return value;
                });

            (await mimic.Object.ValueTask(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithTwelveParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.ValueTask(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12))
                .Returns((int a1, int a2, int a3, int a4, int a5, int a6, int a7, int a8, int a9, int a10, int a11, int a12) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    a4.ShouldBe(t4);
                    a5.ShouldBe(t5);
                    a6.ShouldBe(t6);
                    a7.ShouldBe(t7);
                    a8.ShouldBe(t8);
                    a9.ShouldBe(t9);
                    a10.ShouldBe(t10);
                    a11.ShouldBe(t11);
                    a12.ShouldBe(t12);
                    return value;
                });

            (await mimic.Object.ValueTask(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithThirteenParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.ValueTask(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13))
                .Returns((int a1, int a2, int a3, int a4, int a5, int a6, int a7, int a8, int a9, int a10, int a11, int a12, int a13) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    a4.ShouldBe(t4);
                    a5.ShouldBe(t5);
                    a6.ShouldBe(t6);
                    a7.ShouldBe(t7);
                    a8.ShouldBe(t8);
                    a9.ShouldBe(t9);
                    a10.ShouldBe(t10);
                    a11.ShouldBe(t11);
                    a12.ShouldBe(t12);
                    a13.ShouldBe(t13);
                    return value;
                });

            (await mimic.Object.ValueTask(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithFourteenParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.ValueTask(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14))
                .Returns((int a1, int a2, int a3, int a4, int a5, int a6, int a7, int a8, int a9, int a10, int a11, int a12, int a13, int a14) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    a4.ShouldBe(t4);
                    a5.ShouldBe(t5);
                    a6.ShouldBe(t6);
                    a7.ShouldBe(t7);
                    a8.ShouldBe(t8);
                    a9.ShouldBe(t9);
                    a10.ShouldBe(t10);
                    a11.ShouldBe(t11);
                    a12.ShouldBe(t12);
                    a13.ShouldBe(t13);
                    a14.ShouldBe(t14);
                    return value;
                });

            (await mimic.Object.ValueTask(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithFifteenParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14, int t15)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.ValueTask(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15))
                .Returns((int a1, int a2, int a3, int a4, int a5, int a6, int a7, int a8, int a9, int a10, int a11, int a12, int a13, int a14, int a15) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    a4.ShouldBe(t4);
                    a5.ShouldBe(t5);
                    a6.ShouldBe(t6);
                    a7.ShouldBe(t7);
                    a8.ShouldBe(t8);
                    a9.ShouldBe(t9);
                    a10.ShouldBe(t10);
                    a11.ShouldBe(t11);
                    a12.ShouldBe(t12);
                    a13.ShouldBe(t13);
                    a14.ShouldBe(t14);
                    a15.ShouldBe(t15);
                    return value;
                });

            (await mimic.Object.ValueTask(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15)).ShouldBe(value);
        }

        [Theory, AutoData]
        public static async Task Returns_WithSixteenParamsAndValueFunction_ShouldReturnValue(int value, int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14, int t15, int t16)
        {
            var mimic = new Mimic<ISubject>();

            mimic.Setup(m => m.ValueTask(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16))
                .Returns((int a1, int a2, int a3, int a4, int a5, int a6, int a7, int a8, int a9, int a10, int a11, int a12, int a13, int a14, int a15, int a16) =>
                {
                    a1.ShouldBe(t1);
                    a2.ShouldBe(t2);
                    a3.ShouldBe(t3);
                    a4.ShouldBe(t4);
                    a5.ShouldBe(t5);
                    a6.ShouldBe(t6);
                    a7.ShouldBe(t7);
                    a8.ShouldBe(t8);
                    a9.ShouldBe(t9);
                    a10.ShouldBe(t10);
                    a11.ShouldBe(t11);
                    a12.ShouldBe(t12);
                    a13.ShouldBe(t13);
                    a14.ShouldBe(t14);
                    a15.ShouldBe(t15);
                    a16.ShouldBe(t16);
                    return value;
                });

            (await mimic.Object.ValueTask(t1, t2, t3, t4, t5, t6, t7, t8, t9, t10, t11, t12, t13, t14, t15, t16)).ShouldBe(value);
        }
    }

    // ReSharper disable once MemberCanBePrivate.Global
    internal interface ISubject
    {
        public Task<int> Task();

        public Task<int> Task(int t);

        public Task<int> Task(int t1, int t2);

        public Task<int> Task(int t1, int t2, int t3);

        public Task<int> Task(int t1, int t2, int t3, int t4);

        public Task<int> Task(int t1, int t2, int t3, int t4, int t5);

        public Task<int> Task(int t1, int t2, int t3, int t4, int t5, int t6);

        public Task<int> Task(int t1, int t2, int t3, int t4, int t5, int t6, int t7);

        public Task<int> Task(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8);

        public Task<int> Task(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9);

        public Task<int> Task(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10);

        public Task<int> Task(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11);

        public Task<int> Task(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12);

        public Task<int> Task(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13);

        public Task<int> Task(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14);

        public Task<int> Task(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14, int t15);

        public Task<int> Task(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14, int t15, int t16);

        public ValueTask<int> ValueTask();

        public ValueTask<int> ValueTask(int t);

        public ValueTask<int> ValueTask(int t1, int t2);

        public ValueTask<int> ValueTask(int t1, int t2, int t3);

        public ValueTask<int> ValueTask(int t1, int t2, int t3, int t4);

        public ValueTask<int> ValueTask(int t1, int t2, int t3, int t4, int t5);

        public ValueTask<int> ValueTask(int t1, int t2, int t3, int t4, int t5, int t6);

        public ValueTask<int> ValueTask(int t1, int t2, int t3, int t4, int t5, int t6, int t7);

        public ValueTask<int> ValueTask(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8);

        public ValueTask<int> ValueTask(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9);

        public ValueTask<int> ValueTask(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10);

        public ValueTask<int> ValueTask(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11);

        public ValueTask<int> ValueTask(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12);

        public ValueTask<int> ValueTask(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13);

        public ValueTask<int> ValueTask(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14);

        public ValueTask<int> ValueTask(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14, int t15);

        public ValueTask<int> ValueTask(int t1, int t2, int t3, int t4, int t5, int t6, int t7, int t8, int t9, int t10, int t11, int t12, int t13, int t14, int t15, int t16);
    }
}
