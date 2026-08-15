# Football-Platformer

A football-themed Unity platformer (C# / URP). Play from **Main Menu** → **SampleScene**, then **Exit Scene** / **Score Board**.

## Play

Open `FootballGame/` in Unity 2022.3 (URP). Build scenes are listed in `FootballGame/ProjectSettings/EditorBuildSettings.asset`.

Live player in `SampleScene` is the Starter Assets capsule (`TestPlayer`, tag `Player`). Leftover `BallmanController` disables itself when that first-person player is present.

## Tests / CI

Unity-free score helpers live in `FootballGame/Assets/Scripts/ScoreRules.cs` and are covered by:

```bash
dotnet test tests/FootballPlatformer.Tests/FootballPlatformer.Tests.csproj
```

GitHub Actions (`.github/workflows/ci.yml`) runs those tests, rejects tracked player dumps (`scripts/check-repo-hygiene.sh`), and scans the repo with Gitleaks.

## Repo layout

The Unity project is nested under `FootballGame/`. Linux player output (`Dash and Grab_Data/`, `UnityPlayer.so`, zip dumps) is gitignored and must stay untracked.
