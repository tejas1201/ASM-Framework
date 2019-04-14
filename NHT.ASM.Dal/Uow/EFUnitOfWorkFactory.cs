using NHT.ASM.Infrastructure;

namespace NHT.ASM.Dal.Uow
{
  /// <summary>
  /// Creates new instances of an EF unit of Work.
  /// </summary>
  public class EfUnitOfWorkFactory: IUnitOfWorkFactory<AsmContext>
  {
    /// <inheritdoc />
    public IUnitOfWork Create(AsmContext context)
    {
      return new EfUnitOfWork(context);
    }

    }
}
