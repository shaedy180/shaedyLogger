#!/usr/bin/env bash
# Setup GitHub labels for the shaedyLogger repository.
# Requires: gh CLI (https://cli.github.com/) authenticated with repo access.
#
# Usage: bash .github/setup-labels.sh
# This script is idempotent. Existing labels will be updated to match.

REPO="shaedy180/shaedyLogger"

labels=(
  # Issue template labels
  "bug|d73a4a|Something is not working"
  "enhancement|a2eeef|New feature or improvement"
  "documentation|0075ca|Documentation changes"
  "question|d876e3|General question or discussion"

  # Triage / status
  "needs-triage|ededed|Needs initial review"
  "confirmed|0e8a16|Bug has been confirmed"
  "wontfix|ffffff|Will not be addressed"
  "duplicate|cfd3d7|Duplicate of another issue"
  "invalid|e4e669|Not a valid issue"

  # Priority
  "priority: low|c5def5|Low priority"
  "priority: medium|fbca04|Medium priority"
  "priority: high|ff7619|High priority"
  "priority: critical|b60205|Critical priority"

  # Scope
  "scope: gameplay|bfdadc|In-game behavior and mechanics"
  "scope: config|bfdadc|Configuration and settings"
  "scope: chat|bfdadc|Chat messages and localization"
  "scope: performance|bfdadc|Performance and optimization"

  # Workflow
  "good first issue|7057ff|Good for newcomers"
  "help wanted|008672|Extra attention is needed"

  # Platform
  "platform: linux|c2e0c6|Linux-specific"
  "platform: windows|c2e0c6|Windows-specific"
)

for entry in "${labels[@]}"; do
  IFS='|' read -r name color description <<< "$entry"
  echo "Creating/updating label: $name"
  gh label create "$name" --repo "$REPO" --color "$color" --description "$description" --force
done

echo "Done. All labels have been created or updated."