#!/usr/bin/env bash
set -euo pipefail

root="$(git rev-parse --show-toplevel)"
cd "$root"

patterns=(
  'Dash and Grab_Data/'
  'Dash and Grab_BurstDebugInformation_DoNotShip/'
  'UnityPlayer.so'
  'Assets.zip'
  'ProjectSettings.zip'
  'FootballGame/obj/'
)

tracked="$(git ls-files)"
failed=0

for pattern in "${patterns[@]}"; do
  matches="$(printf '%s\n' "$tracked" | grep -F "$pattern" || true)"
  if [[ -n "$matches" ]]; then
    echo "Tracked generated artifact matches '${pattern}':" >&2
    printf '%s\n' "$matches" >&2
    failed=1
  fi
done

if [[ ! -f tests/FootballPlatformer.Tests/FootballPlatformer.Tests.csproj ]]; then
  echo "Missing tests/FootballPlatformer.Tests/FootballPlatformer.Tests.csproj" >&2
  failed=1
fi

if [[ "$failed" -ne 0 ]]; then
  exit 1
fi

echo "Repo hygiene OK"
