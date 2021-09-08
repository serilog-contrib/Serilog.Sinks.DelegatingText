# Automation

## commit-msg git hook
"commit-msg" is a git hook that asserts that angular commit guidelines are enforced. If the commit message does not adhere, the commit is blocked.
The git hook is copied to the git hooks directory using an VS after-build CopyTask.

source: https://medium.com/yasser-dev/automate-and-enforce-conventional-commits-for-net-based-projects-a322be7a1eb7

## pre-commit git hook
"pre-commit" is a git hook that asserts that no file is committed that contains the "!NOCOMMIT" text. It's an easy way to prevent credentials and other secrets from getting committed.
