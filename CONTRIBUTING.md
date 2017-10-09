# Contributing #

## Tickets (Issues) ##

For each task a ticket should be created and tagged appropriately. Each ticket should have an assignee, type label, assigned sprint (milestone), and epic (project). The title of the ticket should conform to the following format "{compexity} - {Brief description / name}", where the complexity is an estimate of how long the task should take (as a power of 2) with 2 being a quick fix and 32 being a weeks work. The body of the ticket should have a description of the issue and a link to the branch where the ticket is being worked on. If the ticket is a non-trivial task, a design should be provided to clarify exactly what is to be done as part of the completion of the ticket. This design should then be reviewed by another member of the team before implementation begins.

## Swimlanes ##

For each epic (project) there will be a swimlane, and tickets should move through the swimlane during their development.

## Branches ##

For each ticket, while it is being worked on, there should be a branch for just that ticket. The branch should be named with the following template: "#{issue-number} {description}"

## Pull Requests ##

When work is completed on a ticket, a pull request should be made, and the ticket should be moved into the 'to review' swimlane. At least one other memeber of the team must review the code change before it can be merged into master.
