﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class Tashvigh
    {
        public static OperationResult<List<Tashvigh_Tbl>> Select(string search = "")
        {
            SAPDbDataContext dataContext = new SAPDbDataContext();
            try
            {
                var query = dataContext.Tashvigh_Tbls.Where(p => p.TashvighDate==search).ToList();
                return new OperationResult<List<Tashvigh_Tbl>>
                {
                    Success = true,
                    Data = query
                };
            }
            catch
            {
                return new OperationResult<List<Tashvigh_Tbl>>
                {
                    Success = false
                };

            }
        }

        public static OperationResult<List<Tashvigh_Tbl>> SelectByMoredTitle(string Title)
        {
            SAPDbDataContext dataContext = new SAPDbDataContext();
            try
            {
                var query = dataContext.Tashvigh_Tbls.Where(p => p.TashvighMoredTypeTitle == Title).ToList();
                return new OperationResult<List<Tashvigh_Tbl>>
                {
                    Success = true,
                    Data = query
                };
            }
            catch
            {
                return new OperationResult<List<Tashvigh_Tbl>>
                {
                    Success = false
                };

            }
        }
        public static OperationResult Delete(int id)
        {
            SAPDbDataContext dataContext = new SAPDbDataContext();
            try
            {
                var tashvigh = dataContext.Tashvigh_Tbls.Where(p => p.Id == id).Single();
                dataContext.Tashvigh_Tbls.DeleteOnSubmit(tashvigh);
                dataContext.SubmitChanges();
                var result = Mored.SelectScore(tashvigh.TashvighMoredTypeTitle);
                if (result.Success)
                {
                    var student = Student.SelectStudent(tashvigh.TashvighStudentCode);
                    if (student.Success)
                    {
                        student.Data.StudentScore -= result.Data;
                        var update = Student.Update(student.Data.StudentCode, student.Data);
                        if (update.Success)
                        {
                            return new OperationResult
                            {
                                Success = true
                            };
                        }
                    }
                }
                return new OperationResult { Success = false };
            }
            catch
            {
                return new OperationResult
                {
                    Success = false
                };
            }
        }
        public static OperationResult Insert(Tashvigh_Tbl tashvigh)
        {
            SAPDbDataContext dataContext = new SAPDbDataContext();
            try
            {
                dataContext.Tashvigh_Tbls.InsertOnSubmit(tashvigh);
                dataContext.SubmitChanges();
                var result = Mored.SelectScore(tashvigh.TashvighMoredTypeTitle);
                if (result.Success)
                {
                    var student = Student.SelectStudent(tashvigh.TashvighStudentCode);
                    if (student.Success)
                    {
                        student.Data.StudentScore += result.Data;
                        var update = Student.Update(student.Data.StudentCode, student.Data);
                        if (update.Success)
                        {
                            return new OperationResult
                            {
                                Success = true
                            };
                        }
                    }
                }
                return new OperationResult
                {
                    Success = false
                };
            }
            catch
            {
                return new OperationResult
                {
                    Success = false
                };
            }


        }
        public static OperationResult Update(Tashvigh_Tbl tashvigh, double lastScore, bool updateMored = false)
        {
            string lastTitle;
            SAPDbDataContext dataContext = new SAPDbDataContext();
            try
            {
                var lastTashvigh = dataContext.Tashvigh_Tbls.Where(p => p.Id == tashvigh.Id).Single();
                lastTitle = lastTashvigh.TashvighMoredTypeTitle; 
                var result = Mored.SelectScore(lastTitle);
                    var result2 = Mored.SelectScore(tashvigh.TashvighMoredTypeTitle);
                lastTashvigh.TashvighElat = tashvigh.TashvighElat;
                lastTashvigh.TashvighEghdamKonande = tashvigh.TashvighEghdamKonande;
                lastTashvigh.TashvighMoredTypeTitle = tashvigh.TashvighMoredTypeTitle;
                dataContext.SubmitChanges();
                if (lastTitle != tashvigh.TashvighMoredTypeTitle || result.Data != lastScore)
                {
                    if (result.Success && result2.Success)
                    {
                        if(updateMored)
                        {
                            var student = Student.SelectStudent(tashvigh.TashvighStudentCode);
                            if (student.Success)
                            {
                                student.Data.StudentScore -= lastScore;
                                student.Data.StudentScore += result2.Data;
                                var update = Student.Update(student.Data.StudentCode, student.Data);
                                if (update.Success)
                                {
                                    return new OperationResult
                                    {
                                        Success = true
                                    };
                                }
                            }
                        }
                        else
                        {
                            var student = Student.SelectStudent(tashvigh.TashvighStudentCode);
                            if (student.Success)
                            {
                                student.Data.StudentScore -= result.Data;
                                student.Data.StudentScore += result2.Data;
                                var update = Student.Update(student.Data.StudentCode, student.Data);
                                if (update.Success)
                                {
                                    return new OperationResult
                                    {
                                        Success = true
                                    };
                                }
                            }
                        }
                       
                    }
                }
                else
                {
                    return new OperationResult
                    {
                        Success = true
                    };
                }
                return new OperationResult
                {
                    Success = false
                };
            }
            catch
            {
                return new OperationResult
                {
                    Success = false
                };
            }
        }

        public static OperationResult<List<Tashvigh_Tbl>> SelectTashvighsStudent(string StudentCode)
        {
            SAPDbDataContext dataContext = new SAPDbDataContext();
            try
            {
                var tashvighs = dataContext.Tashvigh_Tbls.Where(tashvigh => tashvigh.TashvighStudentCode == StudentCode).ToList();
                return new OperationResult<List<Tashvigh_Tbl>>
                {
                    Success = true,
                    Data = tashvighs
                };
            }
            catch (Exception)
            {
                return new OperationResult<List<Tashvigh_Tbl>>
                {
                    Success = false,
                };
            }
        }
        public static OperationResult MinusScore(Tashvigh_Tbl tashvigh, double score)
        {
            SAPDbDataContext dataContext = new SAPDbDataContext();
            try
            {
                var student = Student.SelectStudent(tashvigh.TashvighStudentCode);
                student.Data.StudentScore -= score;
                var update = Student.Update(student.Data.StudentCode, student.Data);
                if (update.Success)
                {
                    return new OperationResult { Success = true };
                }
                else
                {
                    return new OperationResult { Success = false };
                }
            }
            catch (Exception)
            {
                return new OperationResult { Success = false };
            }
        }
    }
}
