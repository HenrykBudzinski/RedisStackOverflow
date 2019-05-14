using FluentValidation.Attributes;
using RedisStackOverflow.Entities.Redis;
using RedisStackOverflow.Entities.Test.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisStackOverflow.Entities.Test
{
    [Validator(typeof(TestEntityValidator))]
    public class TestEntity 
        : RedisDefaultKey<TestEntity, TestEntityValidator>
    {
        public TestEntity()
        {
            Dependent = new DependentTestEntity();
        }

        //public override ulong Id { get; set; }
        public DateTime DataHora { get; set; }
        public DateTime? DataHoraNulo { get; set; }
        public TimeSpan Hora { get; set; }
        public bool ValorBoleano { get; set; }
        public bool? ValorBoleanoNulo { get; set; }
        public byte ValorByte { get; set; }
        public sbyte ValorSbyte { get; set; }
        public int ValorInt { get; set; }
        public int? ValorIntNulo { get; set; }
        public List<DateTime> DataHoraList { get; set; }
        public List<TimeSpan> HoraList { get; set; }
        public List<string> TextoList { get; set; }
        public int DependentId { get; set; }

        public DependentTestEntity Dependent { get; set; }
    }

    [Validator(typeof(DependentTestEntityValidator))]
    public class DependentTestEntity 
        : RedisDefaultKey<DependentTestEntity, DependentTestEntityValidator>
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public DateTime? DataHoraNulo { get; set; }
        public TimeSpan Hora { get; set; }
        public bool ValorBoleano { get; set; }
        public bool? ValorBoleanoNulo { get; set; }
        public byte ValorByte { get; set; }
        public sbyte ValorSbyte { get; set; }
        public int ValorInt { get; set; }
        public int? ValorIntNulo { get; set; }
        public List<DateTime> DataHoraList { get; set; }
        public List<TimeSpan> HoraList { get; set; }
        public List<string> TextoList { get; set; }
    }
}
