using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControleGastos.Core.CoreViewModels
{
    public class ResultViewModel<T>
    {
        public bool Success { get; set; }


        public T Data { get; set; }

        public List<string> Errors { get; set; } = new();

        public ResultViewModel() { }
        public ResultViewModel(bool success, T data, List<string> errors)
        {
            Success = success;
            Data = data;
            Errors = errors;
        }

        public ResultViewModel(bool success, T data)
        {
            Success = success;
            Data = data;
        }

        public ResultViewModel(List<string> errors)
        {
            Errors = errors;
        }

        public ResultViewModel(string error)
        {
            Errors.Add(error);
        }
    }
}
