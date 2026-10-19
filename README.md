
# When should I write async methods?
A: Always if possible especially I/O operations (disk read/writes, network, database operations, API calls, etc)


# How should signatures look:

like this:

public async Task<T> MethodName(params) { }

# How should I call async methods?

await MethodCallAsync();

# What if im calling from synchronus context and cannot change signature?

Then do MethodAsync().GetAwaiter().GetResult();