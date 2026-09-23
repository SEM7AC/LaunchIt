namespace LaunchIt.Resolvers;

public class KillMatch
    {
    public int ProcessId { get; set; }
    public string ProcessName { get; set; } = string.Empty;
    public string FileDescription { get; set; } = string.Empty;
    }

public class KillResolver
    {
    //Logger Delegate
    public Action<string>? Logger { get; set; }

    public string NormalizeInput(string? input)
        {
        if (string.IsNullOrWhiteSpace(input))
            return string.Empty;

        return input.Trim().ToLowerInvariant();
        }

    public List<KillMatch> ResolveByProcessScan(string userText)
        {
        var normalizedInput = NormalizeInput(userText);
        var processes = System.Diagnostics.Process.GetProcesses();
        var matches = new List<KillMatch>();

        foreach (var process in processes)
            {
            try
                {
                var description = process.MainModule?.FileVersionInfo.FileDescription;
                var normalizedDescription = NormalizeInput(description);

                if (normalizedDescription != null &&
                    normalizedDescription.Contains(normalizedInput, StringComparison.OrdinalIgnoreCase))
                    {
                    matches.Add(new KillMatch
                        {
                        ProcessId = process.Id,
                        ProcessName = process.ProcessName,
                        FileDescription = description ?? string.Empty
                        });
                    }
                }
            catch (Exception ex) when (ex is System.ComponentModel.Win32Exception || ex is System.InvalidOperationException)
                {
                // Access denied for protected system processes — ignore them
                Logger?.Invoke($"Access denied for PID {process.Id} ({process.ProcessName}): {ex.Message}");
                }
            }

        return matches;
        }

    }




