Generic Repository and UnitOfWork implementations based on
http://blog.longle.net/2013/05/11/genericizing-the-unit-of-work-pattern-repository-pattern-with-entity-framework-in-mvc/

example usage
int totalUserCount = 0;
_unitOfWork.Repository<User>()
	.Query()
	.Include(u => u.Email)
	.Filter(u => u.Email == email && u.Password == password)
	.GetPage(2, 10, out totalUserCount);

Abandoned this approach since it is adding an abstraction on top of an abstraction just adding more work.
May resume this in the future if it is needed for testing.
Case for this presented by
http://www.nogginbox.co.uk/blog/do-we-need-the-repository-pattern

for now use context interface for mocking
http://www.nogginbox.co.uk/blog/mocking-entity-framework-data-context