# NUnit Reproductions for  Issues in the NUnit Framework

This repository contains a set of reproducible test cases for issues in the NUnit Framework, which can be found in the [NUnit](https://github.com/nunit/nunit) repository.

## How to use this repository

When we you raise an issue, or when you want to contribute a fix, you can use this repository to create a reproducible test case.

1. Create a folder with the name `IssueXXXX` where `XXXX` is the issue number.
2. Add a readme.md file, which contains at the minimum a link to the issue in the NUnit repository.
3. Create your repro solution any way you like.  It should compile and the tests you add should run, failing or not, based on what your case is.

Note that this is not a requirement for adding issues, because some issues are not easily reproduced. Use this when you can reproduce an issue, it makes it much easier for others, including the NUnit team, to understand your issue and hopefully find a fix to it.


