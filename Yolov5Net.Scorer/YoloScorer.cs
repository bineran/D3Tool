﻿using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Yolov5Net.Scorer.Extensions;
using Yolov5Net.Scorer.Models.Abstract;

namespace Yolov5Net.Scorer;

/// <summary>
/// Yolov5 scorer.
/// </summary>
public class YoloScorer<T> : IDisposable where T : YoloModel
{
    private readonly T _model;

    private InferenceSession _inferenceSession { get; set; }

    private InferenceSession GetInferenceSession(int index=0)
    { 
        return inferenceSessions[index];
    }
    /// <summary>
    /// Outputs value between 0 and 1.
    /// </summary>
    private static float Sigmoid(float value)
    {
        return 1 / (1 + (float)Math.Exp(-value));
    }

    /// <summary>
    /// Converts xywh bbox format to xyxy.
    /// </summary>
    private float[] Xywh2xyxy(float[] source)
    {
        var result = new float[4];

        result[0] = source[0] - (source[2] / 2f);
        result[1] = source[1] - (source[3] / 2f);
        result[2] = source[0] + (source[2] / 2f);
        result[3] = source[1] + (source[3] / 2f);

        return result;
    }
    public int LabelCount { get; set; }
    /// <summary>
    /// Returns value clamped to the inclusive range of min and max.
    /// </summary>
    public float Clamp(float value, float min, float max)
    {
        return (value < min) ? min : (value > max) ? max : value;
    }

    /// <summary>
    /// Extracts pixels into tensor for net input.
    /// </summary>
    private Tensor<float> ExtractPixels(Image<Rgba32> image)
    {
        var tensor = new DenseTensor<float>(new[] { 1, 3, _model.Height, _model.Width });

        Parallel.For(0, image.Height, y =>
        {
            Parallel.For(0, image.Width, x =>
            {
                tensor[0, 0, y, x] = image[x, y].R / 255.0F; // r
                tensor[0, 1, y, x] = image[x, y].G / 255.0F; // g
                tensor[0, 2, y, x] = image[x, y].B / 255.0F; // b
            });
        });

        return tensor;
    }

    /// <summary>
    /// Runs inference session.
    /// </summary>
    private DenseTensor<float>[] Inference(Image<Rgba32> image)
    {
        try
        {
            if (image.Width != _model.Width || image.Height != _model.Height)
            {
                image.Mutate(x => x.Resize(_model.Width, _model.Height)); // fit image size to specified input size
            }
            var inputs = new List<NamedOnnxValue> // add image as input
            {  
                NamedOnnxValue.CreateFromTensor("images", ExtractPixels(image))
            };
            
            var result = _inferenceSession.Run(inputs); // run inference

            var output = new List<DenseTensor<float>>();

            foreach (var item in _model.Outputs) // add outputs for processing
            {
                var o = result.FirstOrDefault(x => x.Name == item);
                if (o != null)
                {
                    var dt = o.Value as DenseTensor<float>;
                    if (dt != null)
                    {
                        output.Add(dt);
                    }
                }

            };

            return output.ToArray();
        }
        catch
        {
            return null;
        }
    }
    private IDisposableReadOnlyCollection<DisposableNamedOnnxValue> Run(int index, List<NamedOnnxValue> inputs)
    {
        switch (index)
        {
            case 0:
                lock (object1)
                    return this.GetInferenceSession(index).Run(inputs);
            case 1:
                lock (object2)
                    return this.GetInferenceSession(index).Run(inputs);
            case 2:
                lock (object3)
                    return this.GetInferenceSession(index).Run(inputs);
            case 3:
                lock (object4)
                    return this.GetInferenceSession(index).Run(inputs);
            case 4:
                lock (object5)
                    return this.GetInferenceSession(index).Run(inputs);
            case 5:
                lock (object6)
                    return this.GetInferenceSession(index).Run(inputs);
            case 6:
                lock (object7)
                    return this.GetInferenceSession(index).Run(inputs);
            case 7:
                lock (object8)
                    return this.GetInferenceSession(index).Run(inputs);
            case 8:
                lock (object9)
                    return this.GetInferenceSession(index).Run(inputs);
            case 9:
                lock (object0)
                    return this.GetInferenceSession(index).Run(inputs);



        }
        return null;
    }
    private DenseTensor<float>[] Inference(Image<Rgba32> image,int index)
    {
        try
        {
            if (image.Width != _model.Width || image.Height != _model.Height)
            {
                image.Mutate(x => x.Resize(_model.Width, _model.Height)); // fit image size to specified input size
            }
            var inputs = new List<NamedOnnxValue> // add image as input
            {
                NamedOnnxValue.CreateFromTensor("images", ExtractPixels(image))

            };

            var result = Run(index, inputs); // run inference

            var output = new List<DenseTensor<float>>();

            foreach (var item in _model.Outputs) // add outputs for processing
            {
                var o = result.FirstOrDefault(x => x.Name == item);
                if (o != null)
                {
                    var dt = o.Value as DenseTensor<float>;
                    if (dt != null)
                    {
                        output.Add(dt);
                    }
                }

            };

            return output.ToArray();
        }
        catch
        {
            return null;
        }
    }
    public int Dimensions
    {
        get
        {
            //_model.Dimensions
            return this.LabelCount;
        }
    }
    /// <summary>
    /// Parses net output (detect) to predictions.
    /// </summary>
    private List<YoloPrediction> ParseDetect(DenseTensor<float> output, (int Width, int Height) image)
    {
        var result = new ConcurrentBag<YoloPrediction>();

        var (xGain, yGain) = (_model.Width / (float)image.Width, _model.Height / (float)image.Height); // x, y gains

        var (xPadding, yPadding) = ((_model.Width - (image.Width * xGain)) / 2, (_model.Height - (image.Height * yGain)) / 2); // left, right pads

        var count = (int)output.Length / this.Dimensions;
        Parallel.For(0, count, i =>
        {
            // skip low obj_conf results
            try
            {


                if (output[0, i, 4] <= _model.Confidence) 
                    return;
            }
            catch
            {
                return;
            }
   

            Parallel.For(5, this.Dimensions, j => output[0, i, j] *= output[0, i, 4]);

            Parallel.For(5, this.Dimensions, k =>
            {
     
                if (output[0, i, k] <= _model.MulConfidence) 
                    return; // skip low mul_conf results

                var xMin = (output[0, i, 0] - (output[0, i, 2] / 2) - xPadding) / xGain; // unpad bbox tlx to original
                var yMin = (output[0, i, 1] - (output[0, i, 3] / 2) - yPadding) / yGain; // unpad bbox tly to original
                var xMax = (output[0, i, 0] + (output[0, i, 2] / 2) - xPadding) / xGain; // unpad bbox brx to original
                var yMax = (output[0, i, 1] + (output[0, i, 3] / 2) - yPadding) / yGain; // unpad bbox bry to original

                xMin = Clamp(xMin, 0, image.Width - 0); // clip bbox tlx to boundaries
                yMin = Clamp(yMin, 0, image.Height - 0); // clip bbox tly to boundaries
                xMax = Clamp(xMax, 0, image.Width - 1); // clip bbox brx to boundaries
                yMax = Clamp(yMax, 0, image.Height - 1); // clip bbox bry to boundaries

                var label = _model.Labels[k - 5];

                var prediction = new YoloPrediction(label, output[0, i, k], new(xMin, yMin, xMax - xMin, yMax - yMin));

                result.Add(prediction);
            });
        });

        return result.ToList();
    }

    private List<YoloPrediction> ParseDetect(DenseTensor<float> output, (int Width, int Height) image, string LabelName)
    {
        var result = new ConcurrentBag<YoloPrediction>();

        var (xGain, yGain) = (_model.Width / (float)image.Width, _model.Height / (float)image.Height); // x, y gains

        var (xPadding, yPadding) = ((_model.Width - (image.Width * xGain)) / 2, (_model.Height - (image.Height * yGain)) / 2); // left, right pads

        var count = (int)output.Length / this.Dimensions;
        var labelIndex = this._model.Labels.FindIndex(r => r.Name == LabelName);
        if (labelIndex == -1)
        {
            return null;
        }
        labelIndex += 5;


        Parallel.For(0, count, i =>
        {
            // skip low obj_conf results
            try
            {


                if (output[0, i, 4] <= _model.Confidence)
                    return;
            }
            catch
            {
                return;
            }

            output[0, i, labelIndex] *= output[0, i, 4];

            var k = labelIndex;


            if (output[0, i, k] <= _model.MulConfidence)
                return; // skip low mul_conf results

            var xMin = (output[0, i, 0] - (output[0, i, 2] / 2) - xPadding) / xGain; // unpad bbox tlx to original
            var yMin = (output[0, i, 1] - (output[0, i, 3] / 2) - yPadding) / yGain; // unpad bbox tly to original
            var xMax = (output[0, i, 0] + (output[0, i, 2] / 2) - xPadding) / xGain; // unpad bbox brx to original
            var yMax = (output[0, i, 1] + (output[0, i, 3] / 2) - yPadding) / yGain; // unpad bbox bry to original

            xMin = Clamp(xMin, 0, image.Width - 0); // clip bbox tlx to boundaries
            yMin = Clamp(yMin, 0, image.Height - 0); // clip bbox tly to boundaries
            xMax = Clamp(xMax, 0, image.Width - 1); // clip bbox brx to boundaries
            yMax = Clamp(yMax, 0, image.Height - 1); // clip bbox bry to boundaries

            var label = _model.Labels[k - 5];

            var prediction = new YoloPrediction(label, output[0, i, k], new(xMin, yMin, xMax - xMin, yMax - yMin));

            result.Add(prediction);

        });

        return result.ToList();
    }

    /// <summary>
    /// Parses net outputs (sigmoid) to predictions.
    /// </summary>
    private List<YoloPrediction> ParseSigmoid(DenseTensor<float>[] output, (int Width, int Height) image)
    {
        var result = new ConcurrentBag<YoloPrediction>();

        var (xGain, yGain) = (_model.Width / (float)image.Width, _model.Height / (float)image.Height); // x, y gains

        var (xPadding, yPadding) = ((_model.Width - (image.Width * xGain)) / 2, (_model.Height - (image.Height * yGain)) / 2); // left, right pads

        Parallel.For(0, output.Length, i => // iterate model outputs
        {
            var shapes = _model.Shapes[i]; // shapes per output

            Parallel.For(0, _model.Anchors[0].Length, a => // iterate anchors
            {
                Parallel.For(0, shapes, y => // iterate shapes (rows)
                {
                    Parallel.For(0, shapes, x => // iterate shapes (columns)
                    {
                        var offset = ((shapes * shapes * a) + (shapes * y) + x) * this.Dimensions;

                        var buffer = output[i].Skip(offset).Take(this.Dimensions).Select(Sigmoid).ToArray();

                        if (buffer[4] <= _model.Confidence) return; // skip low obj_conf results

                        var scores = buffer.Skip(5).Select(b => b * buffer[4]).ToList(); // mul_conf = obj_conf * cls_conf

                        var mulConfidence = scores.Max(); // max confidence score

                        if (mulConfidence <= _model.MulConfidence) return; // skip low mul_conf results

                        var rawX = ((buffer[0] * 2) - 0.5f + x) * _model.Strides[i]; // predicted bbox x (center)
                        var rawY = ((buffer[1] * 2) - 0.5f + y) * _model.Strides[i]; // predicted bbox y (center)

                        var rawW = (float)Math.Pow(buffer[2] * 2, 2) * _model.Anchors[i][a][0]; // predicted bbox w
                        var rawH = (float)Math.Pow(buffer[3] * 2, 2) * _model.Anchors[i][a][1]; // predicted bbox h

                        var xyxy = Xywh2xyxy(new[] { rawX, rawY, rawW, rawH });

                        var xMin = Clamp((xyxy[0] - xPadding) / xGain, 0, image.Width - 0); // unpad, clip tlx
                        var yMin = Clamp((xyxy[1] - yPadding) / yGain, 0, image.Height - 0); // unpad, clip tly
                        var xMax = Clamp((xyxy[2] - xPadding) / xGain, 0, image.Width - 1); // unpad, clip brx
                        var yMax = Clamp((xyxy[3] - yPadding) / yGain, 0, image.Height - 1); // unpad, clip bry

                        var label = _model.Labels[scores.IndexOf(mulConfidence)];

                        var prediction = new YoloPrediction(label, mulConfidence, new(xMin, yMin, xMax - xMin, yMax - yMin));

                        result.Add(prediction);
                    });
                });
            });
        });

        return result.ToList();
    }

    /// <summary>
    /// Parses net outputs (sigmoid or detect layer) to predictions.
    /// </summary>
    private List<YoloPrediction> ParseOutput(DenseTensor<float>[] output, (int Width, int Height) image)
    {
        return _model.UseDetect ? ParseDetect(output[0], image) : ParseSigmoid(output, image);
    }
    private List<YoloPrediction> ParseOutput(DenseTensor<float>[] output, (int Width, int Height) image,string LabelName)
    {
        return _model.UseDetect ? ParseDetect(output[0], image,LabelName) : ParseSigmoid(output, image);
    }

    /// <summary>
    /// Removes overlapped duplicates (nms).
    /// </summary>
    private List<YoloPrediction> Suppress(List<YoloPrediction> items)
    {
        var result = new List<YoloPrediction>(items);

        foreach (var item in items) // iterate every prediction
        {
            foreach (var current in result.ToList().Where(current => current != item)) // make a copy for each iteration
            {
                var (rect1, rect2) = (item.Rectangle, current.Rectangle);

                var intersection = RectangleF.Intersect(rect1, rect2);

                var intArea = intersection.Area(); // intersection area
                var unionArea = rect1.Area() + rect2.Area() - intArea; // union area
                var overlap = intArea / unionArea; // overlap ratio

                if (overlap >= _model.Overlap)
                {
                    if (item.Score >= current.Score)
                    {
                        result.Remove(current);
                    }
                }
            }
        }

        return result;
    }

    /// <summary>
    /// Runs object detection.
    /// </summary>
    public List<YoloPrediction> Predict(Image<Rgba32> image)
    {
        return Suppress(ParseOutput(Inference(image.Clone()), (image.Width, image.Height)));
    }

    /// <summary>
    /// Runs object detection.
    /// </summary>
    public List<YoloPrediction> Predict(Image<Rgba32> image,string labelName,int i)
    {

        var img = Inference(image,i);
        if (img == null)
            return new List<YoloPrediction>();
        return Suppress(ParseOutput(img, (image.Width, image.Height),labelName));
    }

    /// <summary>
    /// Creates new instance of YoloScorer.
    /// </summary>
    public YoloScorer()
    {
        _model = Activator.CreateInstance<T>();
    }

    /// <summary>
    /// Creates new instance of YoloScorer with weights path and options.
    /// </summary>
    public YoloScorer(string weights, SessionOptions opts = null) : this()
    {
        _inferenceSession = new InferenceSession(File.ReadAllBytes(weights), opts ?? new SessionOptions());
    }
    public List<InferenceSession> inferenceSessions { get; set; } = new List<InferenceSession>();

    public object object0 = new object();
    public object object1 = new object();
    public object object2 = new object();
    public object object3 = new object();
    public object object4 = new object();
    public object object5 = new object();
    public object object6 = new object();
    public object object7 = new object();
    public object object8 = new object();
    public object object9 = new object();

    public YoloScorer(string weights,int labelCount,int inferenceCount=1, SessionOptions opts = null) : this()
    {
        this.LabelCount=labelCount+5;
        var bytes = File.ReadAllBytes(weights);
        _inferenceSession = new InferenceSession(bytes, opts ?? new SessionOptions());


        inferenceSessions.Add(_inferenceSession);
        for (int i=1;i< inferenceCount;i++)
        {
            inferenceSessions.Add(new InferenceSession(bytes, opts ?? new SessionOptions()));
        }


    }

    /// <summary>
    /// Creates new instance of YoloScorer with weights stream and options.
    /// </summary>
    public YoloScorer(Stream weights, SessionOptions opts = null) : this()
    {
        using (var reader = new BinaryReader(weights))
        {
            _inferenceSession = new InferenceSession(reader.ReadBytes((int)weights.Length), opts ?? new SessionOptions());
        }
    }

    /// <summary>
    /// Creates new instance of YoloScorer with weights bytes and options.
    /// </summary>
    public YoloScorer(byte[] weights, SessionOptions opts = null) : this()
    {
        _inferenceSession = new InferenceSession(weights, opts ?? new SessionOptions());
    }

    /// <summary>
    /// Disposes YoloScorer instance.
    /// </summary>
    public void Dispose()
    {
        _inferenceSession.Dispose();
        //foreach (var inferenceSession in inferenceSessions)
        //{ inferenceSession.Dispose(); }
    }
}
