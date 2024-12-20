namespace checkredisnull
{
    internal class rudfoss_rr_Tests
    {
        [Test]
        public void SupportsSubscopes()
        {
            var scope = PermissionScope.New("app");
            Assert.That(scope, Is.Not.Null);
            var expected = PermissionScope.New("app");
            Assert.That(expected, Is.Not.Null);
            Assert.That(scope, Is.EqualTo(expected));
        }

        public record PermissionScope
        {
            /// <summary>
            /// The full scope name.
            /// </summary>
            public string Name { get; private init; }

            private PermissionScope(string name) { Name = name; }

            public static PermissionScope New(string value)
            {
                return new(value);
            }

            public static implicit operator string(PermissionScope scope) => scope.Name;
        }
    }
}
