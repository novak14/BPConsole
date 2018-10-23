using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BakalarskaPraceConsole
{
    public interface IMainService
    {
        string[] GetSync();
        Task<string[]> GetAsync();
        string[] GetProc();

        string[] InsertSync();
        Task<string[]> InsertAsync();
        string[] InsertProc();

        string[] UpdateSync();
        Task<string[]> UpdateAsync();
        string[] UpdateProc();

        string[] DeleteSync();
        Task<string[]> DeleteAsync();
        string[] DeleteProc();

    }
}
