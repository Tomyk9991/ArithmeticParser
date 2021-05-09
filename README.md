# ArithmeticParser
Basic arithmetic operations parser written in C#

##Tests:

```c#
private Compiler compiler;
        
        public BasicArithmetics()
        {
            compiler = new Compiler("");
        }
        
        [Fact]
        public void BasicExpression_0()
        {
            double value = compiler.Evaluate("3 + 3");
            Assert.Equal(6, value);
        }
        
        [Fact]
        public void BasicExpression_1()
        {
            double value = compiler.Evaluate("(2 + (3 + 4))");
            Assert.Equal(9, value);
        }

        [Fact]
        public void BasicExpression_2()
        {
            double value = compiler.Evaluate("(2 + (3 + (4 + (5 + 6))))");
            Assert.Equal(20, value);
        }
        
        [Fact]
        public void BasicExpression_3()
        {
            double value = compiler.Evaluate("(2) + (3) + (4)");
            Assert.Equal(9, value);
        }
        
        [Fact]
        public void BasicExpression_4()
        {
            double value = compiler.Evaluate("(2 + 3) + 4");
            Assert.Equal(9, value);
        }

        [Fact]
        public void BasicExpression_5()
        {
            double value = compiler.Evaluate("((4 - 2^3 + 1) * -sqrt(3*3+4*4)) / 2");
            Assert.Equal(7.5, value);
        }
```
