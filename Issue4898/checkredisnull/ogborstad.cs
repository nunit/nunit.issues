namespace Ogborstad;

internal class Test
{
    [Test]
    public void MongoDBObjectId_equality_exception()
    {
        var objectId1 = MongoDB.Bson.ObjectId.GenerateNewId();
        var objectId2 = MongoDB.Bson.ObjectId.GenerateNewId();

        Assert.That(objectId1, Is.Not.EqualTo(objectId2));
    }
}
