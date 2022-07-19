using Framework.Exceptions;
using Microsoft.AspNetCore.Http;

namespace Framework.Helpers;

public class FileManager
{
    public static async Task SaveAsync(string absoluteAddress, string fileName, IFormFile file, CancellationToken cancellationToken)
    {
        if (file == null || file.Length == 0)
            throw new Dexception(Situation.Make(SitKeys.BadRequest),
                new List<KeyValuePair<string, string>> {new(":پیام:", "فایل برای ذخیره شدن، ارسال نشده یا خالی ارسال شده است.")});

        var saveRoot = GlobalConfig.Config["FileStorage:SaveRoot"];
        var fullPath = Path.Combine(saveRoot, absoluteAddress, fileName);

        Directory.CreateDirectory(Path.Combine(saveRoot, absoluteAddress));
        await using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream, cancellationToken);
    }
}